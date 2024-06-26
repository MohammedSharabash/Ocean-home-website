using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Ocean_Home.Helper;
using Ocean_Home.Models.Enums;
using static System.Net.Mime.MediaTypeNames;
using Ocean_Home.Models.ViewModel;

namespace Ocean_Home.Controllers
{
    public class ImageController : Controller
    {
        private readonly IGeneric<Project> _Project;
        private readonly IGeneric<ProjectImage> _Image;
        private readonly ICRUD<ProjectImage> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageController(IGeneric<Project> Project, IGeneric<ProjectImage> Image, ICRUD<ProjectImage> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _Project = Project;
            _Image = Image;
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
            var files = HttpContext.Request.Form.Files;
            var Images = _Image.Get(x => x.IsDeleted && x.ProjectId == model.ProjectId).OrderBy(x => x.Sort).ToList();

            var sort = Images == null || Images.Count() < 1 ? 0 : Images.Last().Sort;
            sort += 1;
            foreach (var file in files)
            {
                if (file != null)
                {
                    // رفع الملف واستخراج رابط الصورة
                    var imageUrl = await MediaControl.Upload(FilePath.Project, file, _webHostEnvironment);
                    // إضافة رابط الصورة إلى نموذج البيانات
                    ProjectImage image = new ProjectImage()
                    {
                        ProjectId = model.ProjectId,
                        ImageUrl = imageUrl,
                        Sort = sort,
                    };
                    await _Image.Add(image);
                    //
                }
                sort++;
            }
            return RedirectToAction(nameof(Index), new { id = model.ProjectId });
        }
        public async Task<IActionResult> Edit(long id)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!await _Image.IsExist(x => x.Id == id))
                return NotFound();
            var image = _Image.Get(x => x.Id == id).First();
            ImageVM vm = new ImageVM()
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl,
                ProjectId = image.ProjectId,
                Sort = image.Sort,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ImageVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest("حدث خطاء اثناء ادخال البيانات");
            if (model.Id > 0)
            {
                var Image = _Image.Get(x => x.Id == model.Id).First();

                var file = HttpContext.Request.Form.Files.GetFile("Image");
                if (file != null)
                {
                    Image.ImageUrl = await MediaControl.Upload(FilePath.Project, file, _webHostEnvironment);
                }
                Image.Sort = model.Sort;
                if (!await _Image.Update(Image))
                    return BadRequest("حدث خطاء ما من فضلك حاول لاحقاً");
                await _CRUD.Update(Image.Id);
            }
            return RedirectToAction(nameof(Index), new { id = model.ProjectId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteItem(long id)
        {
            if (!await _Image.IsExist(x => x.Id == id))
            {
                return Json(new { success = false, message = "هذه الصوره غير موجوده" });
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
