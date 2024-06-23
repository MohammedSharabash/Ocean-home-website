using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class managerController : Controller
    {
        private readonly IGeneric<Manager> _Manager;
        private readonly ICRUD<Manager> _CRUD;
        public managerController(IGeneric<Manager> Manager, ICRUD<Manager> CRUD)
        {
            _Manager = Manager;
            _CRUD = CRUD;
        }
        public bool auth()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
                return false;
            return true;
        }
        public IActionResult Index(string q)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            switch (q)
            {
                case "deleted":
                    ViewBag.status = "D";
                    return View(_Manager.Get(x => x.IsDeleted).ToList());
                default:
                    return View(_Manager.Get(x => !x.IsDeleted).ToList());
            }
        }
        public async Task<IActionResult> Create(Manager Manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطاء ما اثناء اضافة البيانات");
            }
            if (!await _Manager.Add(Manager))
            {
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(long id)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");

            if (!await _Manager.IsExist(x => x.Id == id))
                return NotFound();
            return View(_Manager.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Manager model)
        {
            if (!ModelState.IsValid)
                return BadRequest("حدث خطاء اثناء ادخال البيانات");

            if (!await _Manager.Update(model))
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            await _CRUD.Update(model.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            if (!await _Manager.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذه الدورة التدريبية غير موجوده" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
                }
                if (_Manager.Get(x => x.Id == id).First().IsDeleted)
                    return Json(new { success = true, message = "تم حذف الوظيفة بنجاح لاسترجاعة قم بالتوجهه الوظائف المحذوفة " });
                else
                    return Json(new { success = true, message = "تم استراجاع الوظيفة بنجاحة " });

            }

        }
    }
}
