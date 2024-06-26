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
    public class ContactUsController : Controller
    {
        private readonly IGeneric<ContactUS> _ContactUS;
        private readonly ICRUD<ContactUS> _CRUD;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContactUsController(IGeneric<ContactUS> ContactUS, ICRUD<ContactUS> CRUD, IWebHostEnvironment webHostEnvironment)
        {
            _ContactUS = ContactUS;
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
            if (!await _ContactUS.IsExist(x => x.Id == id))
            {
                ContactUS model = new ContactUS()
                {
                    Location = ".",
                    CallDirectly_Phone = ".",
                    MobilePhones = ".",
                    WorkHours = ".",
                    Logo = ".",
                    Instagram = ".",
                    Tiktok = ".",
                };
                await _ContactUS.Add(model);
            }
            if (!await _ContactUS.IsExist(x => x.Id == id))
                return NotFound();
            return View(_ContactUS.Get(x => x.Id == id).First());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ContactUS department)
        {
            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                department.Logo = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }

            if (!await _ContactUS.Update(department))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(department.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
