using Ocean_Home.Helper;
using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly IGeneric<Slider> _Slider;
        private readonly ICRUD<Slider> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContactUsController(IGeneric<Slider> Slider, ICRUD<Slider> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _Slider = Slider;
            _CRUD = CRUD;
            _webHostEnvironment = webHostEnvironment;
        }
        public bool auth()
        {
            if (!User.IsInRole("Admin"))
            {
                return false;
            }
            return true;
        }
        public IActionResult Index(string q)
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }
            if (!auth())
                return RedirectToAction("Login", "cp");

            if (q == "deleted")
            {
                ViewBag.State = "D";
                return View(_Slider.Get(x => x.IsDeleted).OrderBy(x => x.Sort).ToList());
            }
            return View(_Slider.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطاء ما اثناء اضافة البيانات");
            }
            var file = HttpContext.Request.Form.Files.GetFile("ImageFile");
            if (file != null)
            {
                model.ImageUrl = await MediaControl.Upload(FilePath.Slider, file, _webHostEnvironment);
            }
            if (!await _Slider.Add(model))
            {
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(long id)

        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!await _Slider.IsExist(x => x.Id == id))
                return NotFound();
            return View(_Slider.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Slider department)
        {

            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                department.ImageUrl = await MediaControl.Upload(FilePath.Slider, file, _webHostEnvironment);
            }
            if (!await _Slider.Update(department))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(department.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _Slider.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذا السلايدر غير موجود" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطأ ما من فضلك حاول لاحقاً " });
                }
                if (_Slider.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف الغلاف بنجاح لاسترجاعه قم بالتوجهه الغلافات المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع السلايدر بنجاحة " });

            }

        }
    }
}
