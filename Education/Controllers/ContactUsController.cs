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
using System.IO;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Edit(ContactUS model, IFormFile PDF)
        {
            var file = HttpContext.Request.Form.Files.GetFile("Image");
            if (file != null)
            {
                model.Logo = await MediaControl.Upload(FilePath.System, file, _webHostEnvironment);
            }
            if (PDF != null && PDF.Length > 0)
            {
                var fileName = Path.GetFileName(PDF.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await PDF.CopyToAsync(stream);
                }
                model.PDF = filePath;
            }
            if (!await _ContactUS.Update(model))
                return BadRequest("حدث خطأ ما من فضلك حاول لاحقاً");
            await _CRUD.Update(model.Id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DownloadPDF()
        {
            // استرجاع كائن ContactUS من قاعدة البيانات
            var model = _ContactUS.Get(x => x.Id == 1).FirstOrDefault();
            if (model == null || string.IsNullOrEmpty(model.PDF))
            {
                return Content("Filename not present");
            }

            if (!System.IO.File.Exists(model.PDF))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(model.PDF);
            var fileName = Path.GetFileName(model.PDF);
            return File(fileBytes, "application/pdf", fileName);
        }

    }
}
