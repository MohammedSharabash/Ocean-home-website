using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Ocean_Home.Models.Enums;
using Ocean_Home.Helper;

namespace Ocean_Home.Controllers
{
    public class AffiliatesController : Controller
    {
        private readonly IGeneric<OurAffiliate> _Affiliates;
        private readonly ICRUD<OurAffiliate> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AffiliatesController(IGeneric<OurAffiliate> Affiliates, ICRUD<OurAffiliate> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _Affiliates = Affiliates;
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
                return View(_Affiliates.Get(x => x.IsDeleted).ToList());
            }
            return View(_Affiliates.Get(x => !x.IsDeleted).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(OurAffiliate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطاء ما اثناء اضافة البيانات");
            }
            //////////////////////////////
            /// الصور

            var file = HttpContext.Request.Form.Files.GetFile("ImageUrl_150x150");
            if (file != null)
            {
                model.ImageUrl_150x150 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_300x300");
            if (file != null)
            {
                model.ImageUrl_300x300 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_370x370");
            if (file != null)
            {
                model.ImageUrl_370x370 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_685x685");
            if (file != null)
            {
                model.ImageUrl_685x685 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_768x768");
            if (file != null)
            {
                model.ImageUrl_768x768 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_1024x1024");
            if (file != null)
            {
                model.ImageUrl_1024x1024 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_1080");
            if (file != null)
            {
                model.ImageUrl_1080 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }

            ///////////
            if (!await _Affiliates.Add(model))
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
            if (!await _Affiliates.IsExist(x => x.Id == id))
                return NotFound();
            return View(_Affiliates.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(OurAffiliate model)
        {

            //////////////////////////////
            /// الصور

            var file = HttpContext.Request.Form.Files.GetFile("ImageUrl_150x150");
            if (file != null)
            {
                model.ImageUrl_150x150 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_300x300");
            if (file != null)
            {
                model.ImageUrl_300x300 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_370x370");
            if (file != null)
            {
                model.ImageUrl_370x370 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_685x685");
            if (file != null)
            {
                model.ImageUrl_685x685 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_768x768");
            if (file != null)
            {
                model.ImageUrl_768x768 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_1024x1024");
            if (file != null)
            {
                model.ImageUrl_1024x1024 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            file = HttpContext.Request.Form.Files.GetFile("ImageUrl_1080");
            if (file != null)
            {
                model.ImageUrl_1080 = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }

            ///////////
            if (!await _Affiliates.Update(model))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(model.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _Affiliates.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذا العنصر غير موجود" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطأ ما من فضلك حاول لاحقاً " });
                }
                if (_Affiliates.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف العنصر بنجاح لاسترجاعه قم بالتوجهه الغلافات المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع العنصر بنجاحة " });

            }

        }
    }
}
