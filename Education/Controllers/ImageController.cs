using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class ImageController : Controller
    {
        private readonly IGeneric<Project> _Project;
        private readonly IGeneric<ProjectImage> _Image;
        private readonly ICRUD<ProjectImage> _CRUD;
        public ImageController(IGeneric<Project> Project, IGeneric<ProjectImage> Image, ICRUD<ProjectImage> CRUD)
        {
            _Project = Project;
            _Image = Image;
            _CRUD = CRUD;
        }

        public bool auth()
        {
            if (!User.IsInRole("Admin"))
            {
                return false;
            }
            return true;
        }
        public async Task<IActionResult> Index(long id, string q)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");

            if (!await _Project.IsExist(x => x.Id == id && (!x.IsDeleted)))
                return NotFound("هذا المشروع غير موجود او محذوف");
            ViewBag.Id = id;
            if (q == "deleted")
            {
                ViewBag.State = "D";
                return View(_Image.Get(x => x.IsDeleted && x.ProjectId == id).OrderBy(x => x.Sort).ToList());
            }
            return View(_Image.Get(x => !x.IsDeleted && (x.ProjectId == id)).OrderBy(x => x.Sort).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectImage model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("حدث خطاء ما اثناء اضافة البيانات");
            }
            if (!await _Image.Add(model))
            {
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            }
            return RedirectToAction(nameof(Index), new { id = model.ProjectId });
        }
        public async Task<IActionResult> Edit(long id)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!await _Image.IsExist(x => x.Id == id))
                return NotFound();
            return View(_Image.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProjectImage Image)
        {
            if (!ModelState.IsValid)
                return BadRequest("حدث خطاء اثناء ادخال البيانات");

            if (!await _Image.Update(Image))
                return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
            await _CRUD.Update(Image.Id);
            return RedirectToAction(nameof(Index), new { id = Image.ProjectId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteItem(long id)
        {
            if (!await _Image.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذه الفيديو غير موجوده" });
            }
            else
            {

                if (!await _CRUD.ToggleDelete(id))
                {
                    return Json(new { success = false, message = "حدث خطاء ما من فضلك حاول لاحقاً " });
                }
                return RedirectToAction(nameof(Index), new { id = _Image.Get(x => x.Id == id).First().ProjectId });

            }

        }
    }
}
