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
    public class EmployeeController : Controller
    {
        private readonly IGeneric<Employee> _Employee;
        private readonly IGeneric<JobsDepartment> _Department;
        private readonly ICRUD<Employee> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IGeneric<Employee> Employee, IGeneric<JobsDepartment> Department, ICRUD<Employee> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _Employee = Employee;
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
                return View(_Employee.Get(x => x.IsDeleted).ToList());
            }
            return View(_Employee.Get(x => !x.IsDeleted).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطأ ما اثناء اضافة البيانات");
            }
            var file = HttpContext.Request.Form.Files.GetFile("ImageFile");
            if (file != null)
            {
                model.ImageUrl = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            if (!await _Employee.Add(model))
            {
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
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
            if (!await _Employee.IsExist(x => x.Id == id))
                return NotFound();
            return View(_Employee.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee Employee)
        {
            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                Employee.ImageUrl = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            if (!await _Employee.Update(Employee))
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            await _CRUD.Update(Employee.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _Employee.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذا الموظف غير موجود" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
                }
                if (_Employee.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف الموظف بنجاح لاسترجاعة قم بالتوجهه العاملين المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع الموظف بنجاحة " });

            }

        }
    }
}
