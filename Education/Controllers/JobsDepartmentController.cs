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
    public class JobsDepartmentController : Controller
    {
        private readonly IGeneric<JobsDepartment> _JobsDepartment;
        private readonly ICRUD<JobsDepartment> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public JobsDepartmentController(IGeneric<JobsDepartment> JobsDepartment, ICRUD<JobsDepartment> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _JobsDepartment = JobsDepartment;
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
                return View(_JobsDepartment.Get(x => x.IsDeleted).OrderBy(x => x.Sort).ToList());
            }
            return View(_JobsDepartment.Get(x => !x.IsDeleted).OrderBy(x => x.Sort).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(JobsDepartment model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطأ ما اثناء اضافة البيانات");
            }

            if (!await _JobsDepartment.Add(model))
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
            if (!await _JobsDepartment.IsExist(x => x.Id == id))
                return NotFound();
            return View(_JobsDepartment.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(JobsDepartment JobsDepartment)
        {

            if (!await _JobsDepartment.Update(JobsDepartment))
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            await _CRUD.Update(JobsDepartment.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _JobsDepartment.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذا القسم غير موجود" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
                }
                if (_JobsDepartment.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف القسم بنجاح لاسترجاعة قم بالتوجهه الاقسام المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع القسم بنجاح " });

            }

        }
    }
}
