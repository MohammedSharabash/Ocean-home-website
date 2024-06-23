using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Ocean_Home.Models.Domain;
using Ocean_Home.Interfaces;
using Ocean_Home.Models.ViewModel;

namespace Ocean_Home.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGeneric<AppUser> _users;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UsersController(IGeneric<AppUser> users,
               UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _users = users;
            _userManager = userManager;
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

        [HttpGet]
        public IActionResult ChangePassword(string UserId)
        {
            return View(new ChangePasswordVm()
            {
                UserId = UserId
            });
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVm model)
        {
            var user = _users.Get(x => x.Id == model.UserId).First();
            var newPassword = _userManager.PasswordHasher.HashPassword(user, model.OldPassword);
            user.PasswordHash = newPassword;
            _users.Update(user);
            return RedirectToAction("Index", "cp");

        }

    }
}
