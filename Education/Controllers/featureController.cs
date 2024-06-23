//using Ocean_Home.Helper;
//using Ocean_Home.Interfaces;
//using Ocean_Home.Models.Domain;
//using Ocean_Home.Models.Enums;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Ocean_Home.Controllers
//{
//    public class featureController : Controller
//    {
//        private readonly IGeneric<feature> _feature;
//        private readonly ICRUD<feature> _CRUD;
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        public featureController(IGeneric<feature> feature, ICRUD<feature> CRUD, IWebHostEnvironment webHostEnvironment)
//        {
//            _feature = feature;
//            _CRUD = CRUD;
//            _webHostEnvironment = webHostEnvironment;
//        }
//        public bool auth()
//        {
//            if (!User.IsInRole("Admin"))
//            {
//                return false;
//            }
//            return true;
//        }
//        public IActionResult Index(string q)
//        {
//            if (!User.IsInRole("Admin"))
//            {
//                return RedirectToAction("Login", "cp");
//            }
//            if (!auth())
//                return RedirectToAction("Login", "cp");

//            if (q == "deleted")
//            {
//                ViewBag.State = "D";
//                return View(_feature.Get(x => x.IsDeleted).OrderBy(x => x.Sort).ToList());
//            }
//            return View(_feature.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList());
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create(feature model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("حدث خطأ ما اثناء اضافة البيانات");
//            }
//            if (!await _feature.Add(model))
//            {
//                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        public async Task<IActionResult> Edit(long id)

//        {
//            if (!User.IsInRole("Admin"))
//            {
//                return RedirectToAction("Login", "cp");
//            }
//            if (!auth())
//                return RedirectToAction("Login", "cp");
//            if (!await _feature.IsExist(x => x.Id == id))
//                return NotFound();
//            return View(_feature.Get(x => x.Id == id).First());
//        }
//        [HttpPost]
//        public async Task<IActionResult> Edit(feature department)
//        {
//            if (!await _feature.Update(department))
//                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
//            await _CRUD.Update(department.Id);
//            return RedirectToAction(nameof(Index));
//        }
//        [HttpDelete]
//        public async Task<IActionResult> Delete(long id)
//        {
//            if (!await _feature.IsExist(x => x.Id == id))
//            {
//                return Json(new { success = false, message = "هذه الميزة غير موجوده" });
//            }
//            else
//            {

//                if (!await _CRUD.ToggleDelete(id))
//                {
//                    return Json(new { success = false, message = "حدث خطأ ما من فضلك حاول لاحقاً " });
//                }
//                if (_feature.Get(x => x.Id == id).First().IsDeleted)
//                    return Json(new { success = true, message = "تم حذف الميزة بنجاح لاسترجاعها قم بالتوجهه المعلومات المحذوفة " });
//                else
//                    return Json(new { success = true, message = "تم استراجاع الميزة بنجاح " });

//            }

//        }
//    }
//}
