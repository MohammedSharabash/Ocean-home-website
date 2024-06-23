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
    public class ProjectsDepartmentController : Controller
    {
        private readonly IGeneric<ProjectsDepartment> _ProjectsDepartment;
        private readonly ICRUD<ProjectsDepartment> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectsDepartmentController(IGeneric<ProjectsDepartment> ProjectsDepartment, ICRUD<ProjectsDepartment> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _ProjectsDepartment = ProjectsDepartment;
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

            if (q == "deleted")
            {
                ViewBag.State = "D";
                return View(_ProjectsDepartment.Get(x => x.IsDeleted).OrderBy(x => x.Sort).ToList());
            }
            return View(_ProjectsDepartment.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectsDepartment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطأ ما اثناء اضافة البيانات");
            }

            if (!await _ProjectsDepartment.Add(model))
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
            if (!await _ProjectsDepartment.IsExist(x => x.Id == id))
                return NotFound();
            return View(_ProjectsDepartment.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectsDepartment ProjectsDepartment)
        {

            if (!await _ProjectsDepartment.Update(ProjectsDepartment))
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            await _CRUD.Update(ProjectsDepartment.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _ProjectsDepartment.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذا القسم غير موجود" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
                }
                if (_ProjectsDepartment.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف القسم بنجاح لاسترجاعة قم بالتوجهه الاقسام المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع القسم بنجاح " });

            }

        }
    }
}
