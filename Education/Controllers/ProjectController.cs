using Ocean_Home.Helper;
using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IGeneric<Project> _Project;
        private readonly IGeneric<ProjectImage> _image;
        private readonly IGeneric<ProjectsDepartment> _Department;
        private readonly ICRUD<Project> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectController(IGeneric<Project> Project, IGeneric<ProjectImage> image, IGeneric<ProjectsDepartment> Department, ICRUD<Project> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _Project = Project;
            _image = image;
            _Department = Department;
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
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }

            var Departments = _Department.GetAll().OrderBy(x => x.Sort).ToList();
            ViewBag.AllDepartments = Departments;
            ViewBag.Departments = new SelectList(Departments.Where(x => !x.IsDeleted).ToList(), "Id", "Name");
            if (q == "deleted")
            {
                ViewBag.State = "D";
                return View(_Project.Get(x => x.IsDeleted).OrderBy(x => x.Sort).ToList());
            }
            return View(_Project.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Project model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطأ ما اثناء اضافة البيانات");
            }
            var files = HttpContext.Request.Form.Files;
            if (files.Count() < 1)
            {
                return BadRequest("حدث خطأ ما اثناء اضافة ألصور");
            }
            if (!await _Project.Add(model))
            {
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            }
            int sort = 1;
            foreach (var file in files)
            {
                if (file != null)
                {
                    // رفع الملف واستخراج رابط الصورة
                    var imageUrl = await MediaControl.Upload(FilePath.Project, file, _webHostEnvironment);
                    // إضافة رابط الصورة إلى نموذج البيانات
                    ProjectImage image = new ProjectImage()
                    {
                        ProjectId = model.Id,
                        ImageUrl = imageUrl,
                        Sort = sort,
                    };
                    await _image.Add(image);
                    //
                }
                sort++;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(long id)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }
            var Departments = _Department.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList();
            ViewBag.Departments = new SelectList(Departments, "Id", "Name");
            if (!await _Project.IsExist(x => x.Id == id))
                return NotFound();
            return View(_Project.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Project Project)
        {

            if (!await _Project.Update(Project))
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            await _CRUD.Update(Project.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _Project.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذا المشروع غير موجود" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
                }
                if (_Project.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف المشروع بنجاح لاسترجاعة قم بالتوجهه المشاريع المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع المشروع بنجاحة " });

            }

        }
    }
}
