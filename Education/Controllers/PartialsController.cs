using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ocean_Home.Models;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.ViewModels;
using Ocean_Home.Interfaces;
using Microsoft.AspNetCore.Identity;
using Ocean_Home.Models.ViewModel;

namespace Ocean_Home.Controllers
{
    public class PartialsController : Controller
    {
        private readonly IGeneric<Notification> _notifiaction;
        private readonly UserManager<AppUser> _userManager;

        public PartialsController(IGeneric<Notification> notifiaction, UserManager<AppUser> userManager)
        {
            _notifiaction = notifiaction;
            _userManager = userManager;
        }
        public ActionResult Header()
        {
            HeaderVM headerVM = new HeaderVM()
            {
                Name = "Admin",
            };
            ViewBag.UserId = _userManager.GetUserId(User);
            var Notifications = _notifiaction.GetAll().OrderByDescending(d => d.CreatedOn).ToList();
            headerVM.NotificationsNumber = Notifications.Count(d => !d.IsSeen && !d.IsDeleted);
            if (Notifications != null && Notifications.Count > 0)
            {
                foreach (var item in Notifications)
                {
                    headerVM.Notifications.Add(new NotificationVM()
                    {
                        Body = item.Body,
                        NotificationType = item.NotificationType,
                        IsSeen = item.IsSeen,
                        Id = item.Id,
                        Title = item.Title,
                        Link = item.NotificationLink,

                    });
                }
            }
            return PartialView(headerVM);
        }
        public ActionResult WebHeader()
        {
            return PartialView();
        }
        //public ActionResult WebFooter()
        //{
        //    var information = _Information.Get(x => x.IsDeleted == false).First();
        //    return PartialView(information);
        //}
        public ActionResult WebHeaderAr()
        {
            return PartialView();
        }
        //public ActionResult WebFooterAr()
        //{
        //    var information = _Information.Get(x => x.IsDeleted == false).First();
        //    return PartialView(information);
        //}

        public ActionResult AdminSideMenu()
        {
            SideMenuVM sideMenuVM = new SideMenuVM()
            {
                Name = "Admin",
                /*Complaints = db.Complaints.Count(w => w.IsDeleted == false && w.IsViewed == false)*/
            };
            return PartialView(sideMenuVM);
        }
    }
}