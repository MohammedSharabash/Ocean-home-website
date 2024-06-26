using Ocean_Home.Helper;
using Ocean_Home.Interfaces;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class AboutUSController : Controller
    {
        private readonly IGeneric<AboutUS> _AboutUS;
        private readonly ICRUD<AboutUS> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AboutUSController(IGeneric<AboutUS> AboutUS, ICRUD<AboutUS> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _AboutUS = AboutUS;
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
        public async Task<IActionResult> Index(long id = 1)
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }
            if (!await _AboutUS.IsExist(x => x.Id == id))
            {
                AboutUS model = new AboutUS()
                {
                    OurVision = ".",
                    OurMessage = ".",
                    Description = ".",
                    ImageUrl = ".",
                    CoverImageUrl = ".",
                };
                await _AboutUS.Add(model);
            }
            if (!await _AboutUS.IsExist(x => x.Id == id))
                return NotFound();
            return View(_AboutUS.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(AboutUS department)
        {
            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                department.ImageUrl = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            var file2 = HttpContext.Request.Form.Files.GetFile("Image2");
            if (file2 != null)
            {
                department.CoverImageUrl = await MediaControl.Upload(FilePath.System, file2, _webHostEnvironment);
            }

            if (!await _AboutUS.Update(department))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(department.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
