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
    public class managerController : Controller
    {
        private readonly IGeneric<Manager> _Manager;
        private readonly ICRUD<Manager> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public managerController(IGeneric<Manager> Manager, ICRUD<Manager> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _Manager = Manager;
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
        public async Task<IActionResult> Index()
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }
            if (!await _Manager.IsExist(x => x.jobTitle == JobTitle.Manager))
            {
                Manager model = new Manager()
                {
                    Name = "محمد محمد محمد",
                    Description = "يمكنك كتابة اي نص هنا , هذا النص يمكن تغييره لاحقا",
                    ImageUrl = ".",
                    jobTitle = JobTitle.Manager,
                };
                await _Manager.Add(model);
            }
            if (!await _Manager.IsExist(x => x.jobTitle == JobTitle.Manager))
                return NotFound();
            return View(_Manager.Get(x => x.jobTitle == JobTitle.Manager).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Manager model)
        {
            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                model.ImageUrl = await MediaControl.Upload(FilePath.Manager, file, _webHostEnvironment);
            }

            if (!await _Manager.Update(model))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(model.Id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ManagingPartner()
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }

            if (!await _Manager.IsExist(x => x.jobTitle == JobTitle.ManagingPartner))
                return RedirectToAction(nameof(create));
            return View(_Manager.Get(x => x.jobTitle == JobTitle.ManagingPartner).First());
        }
        [HttpPost]
        public async Task<IActionResult> EditManagingPartner(Manager model)
        {
            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                model.ImageUrl = await MediaControl.Upload(FilePath.Manager, file, _webHostEnvironment);
            }

            if (!await _Manager.Update(model))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(model.Id);
            return RedirectToAction(nameof(ManagingPartner));
        }
        public async Task<IActionResult> create()
        {
            if (!auth())
                return RedirectToAction("Login", "cp");
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(Manager model)
        {
            if (!await _Manager.IsExist(x => x.jobTitle == JobTitle.ManagingPartner))
            {
                var file = HttpContext.Request.Form.Files.GetFile("Image");
                if (file != null)
                {
                    model.ImageUrl = await MediaControl.Upload(FilePath.Manager, file, _webHostEnvironment);
                }
                model.jobTitle = JobTitle.ManagingPartner;
                await _Manager.Add(model);
            }
            return RedirectToAction(nameof(ManagingPartner));
        }
    }
}
