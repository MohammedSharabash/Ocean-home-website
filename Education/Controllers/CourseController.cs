//using Ocean_Home.Helper;
//using Ocean_Home.Interfaces;
//using Ocean_Home.Models.Domain;
//using Ocean_Home.Models.Enums;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Ocean_Home.Controllers
//{
//    public class CourseController : Controller
//    {
//        private readonly IGeneric<Project> _course;
//        private readonly IGeneric<Department> _Instructor;
//        private readonly ICRUD<Project> _CRUD;
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        public CourseController(IGeneric<Project> course, IGeneric<Department> Instructor, ICRUD<Project> CRUD, IWebHostEnvironment webHostEnvironment)
//        {
//            _course = course;
//            _Instructor = Instructor;
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
//            if (!auth())
//                return RedirectToAction("Login", "cp");
//            if (!User.IsInRole("Admin"))
//            {
//                return RedirectToAction("Login", "cp");
//            }

//            var instructors = _Instructor.GetAll().OrderBy(x => x.Sort).ToList();
//            ViewBag.AllInstructors = instructors;
//            ViewBag.Instructors = new SelectList(instructors.Where(x => !x.IsDeleted).ToList(), "Id", "NameAr");
//            if (q == "deleted")
//            {
//                ViewBag.State = "D";
//                return View(_course.Get(x => x.IsDeleted).OrderBy(x => x.Sort).ToList());
//            }
//            return View(_course.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList());
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create(Project model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("حدث خطأ ما اثناء اضافة البيانات");
//            }
//            var file = HttpContext.Request.Form.Files.GetFile("ImageFile");
//            if (file != null)
//            {
//                model.ImageUrl = await MediaControl.Upload(FilePath.Course, file, _webHostEnvironment);
//            }
//            if (!await _course.Add(model))
//            {
//                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
//            }
//            return RedirectToAction(nameof(Index));
//        }
//        public async Task<IActionResult> Edit(long id)

//        {
//            if (!auth())
//                return RedirectToAction("Login", "cp");
//            if (!User.IsInRole("Admin"))
//            {
//                return RedirectToAction("Login", "cp");
//            }
//            var instructors = _Instructor.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList();
//            ViewBag.Instructors = new SelectList(instructors, "Id", "NameAr");
//            if (!await _course.IsExist(x => x.Id == id))
//                return NotFound();
//            return View(_course.Get(x => x.Id == id).First());
//        }
//        [HttpPost]
//        public async Task<IActionResult> Edit(Project course)
//        {

//            var file = HttpContext.Request.Form.Files.GetFile("Image");
//            if (file != null)
//            {
//                course.ImageUrl = await MediaControl.Upload(FilePath.Course, file, _webHostEnvironment);
//            }
//            if (!await _course.Update(course))
//                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
//            await _CRUD.Update(course.Id);
//            return RedirectToAction(nameof(Index));
//        }
//        [HttpDelete]
//        public async Task<IActionResult> Delete(long id)
//        {
//            if (!await _course.IsExist(x => x.Id == id))
//            {
//                return Json(new { success = false, message = "هذه الدورة التدريبية غير موجوده" });
//            }
//            else
//            {

//                if (!await _CRUD.ToggleDelete(id))
//                {
//                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
//                }
//                if (_course.Get(x => x.Id == id).First().IsDeleted)
//                    return Json(new { success = true, message = "تم حذف الدورة التدريبية بنجاح لاسترجاعة قم بالتوجهه الدورات التدريبية المحذوفة " });
//                else
//                    return Json(new { success = true, message = "تم استراجاع الدورة التدريبية بنجاحة " });

//            }

//        }
//    }
//}
