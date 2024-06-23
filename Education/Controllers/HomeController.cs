using Ocean_Home.Helper;
using Ocean_Home.Interfaces;
using Ocean_Home.Models;
using Ocean_Home.Models.data;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.Dtos;
using Ocean_Home.Models.Enums;
using Ocean_Home.Models.ViewModel;
using Ocean_Home.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Publitio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGeneric<Project> _course;
        private readonly IGeneric<Slider> _Slider;
        private readonly IGeneric<AboutUS> _AboutUS;
        private readonly IGeneric<ProjectImage> _videos;
        private readonly IGeneric<AppUser> _appUser;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserCourse _userCourseService;

        public HomeController(IGeneric<Project> Course, IGeneric<Slider> Slider, IGeneric<AboutUS> AboutUS,
          IUserCourse userCourseService, IConfiguration configuration, IGeneric<ProjectImage> Videos, IGeneric<AppUser> AppUser, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            //_TermsAndCondition = TermsAndCondition;
            //_course = Course;
            //_Instructor = Instructor;
            //_UserFeedBacks = UserFeedBacks;
            //_Slider = Slider;
            //_Information = Information;
            //_feature = feature;
            //_AboutUS = AboutUS;
            //_userCourse = UserCourse;
            //_PublitioAccount = publitioAccount;
            _videos = Videos;
            _appUser = AppUser;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userCourseService = userCourseService;
        }


        //#region Arabic
        //public async Task<IActionResult> IndexAr()
        //{
        //    var UserId = _userManager.GetUserId(User);
        //    IndexVm vm = new IndexVm();
        //    vm.Sliders = _Slider.Get(x => x.IsDeleted == false).ToList();
        //    vm.information = _Information.Get(x => x.IsDeleted == false).First();
        //    vm.features = _feature.Get(x => x.IsDeleted == false).ToList();
        //    vm.aboutUS = _AboutUS.Get(x => x.IsDeleted == false).First();
        //    vm.Courses = _course.Get(x => x.IsDeleted == false && x.InstructorId != null && !x.Instructor.IsDeleted).ToList();
        //    vm.UsersCourses = _userCourseService.GetUserCourse(x => !x.IsDeleted).ToList();
        //    vm.UserFeedBacks = _UserFeedBacks.Get(x => x.IsDeleted == false).ToList();
        //    vm.Instructors = _Instructor.Get(x => x.IsDeleted == false).ToList();
        //    foreach (var co in vm.Courses)
        //    {
        //        var instructor = _Instructor.Get(x => x.IsDeleted == false && x.Id == co.InstructorId).FirstOrDefault();
        //        co.Instructor = instructor;
        //        if (UserId != null && await _userCourse.IsExist(x => x.UserId == UserId && x.CourseId == co.Id && x.IsDeleted == false))
        //        {
        //            var userco = _userCourse.Get(x => x.IsDeleted == false && x.UserId == UserId && x.CourseId == co.Id).FirstOrDefault();

        //            switch (userco.Status)
        //            {
        //                case CourseUserStatus.Pending:
        //                    co.Sort = 0;
        //                    break;
        //                case CourseUserStatus.Accepted:
        //                    co.Sort = 1;
        //                    break;
        //                case CourseUserStatus.Rejected:
        //                    co.Sort = 2;
        //                    break;

        //            }
        //        }
        //        else
        //        {
        //            co.Sort = 10;
        //        }
        //        co.Images = _videos.Get(x => x.CourseId == co.Id && x.IsDeleted == false).ToList();
        //    }

        //    return View(vm);
        //}
        //public IActionResult LogInAr()
        //{
        //    if (User.Identity.IsAuthenticated)
        //        return RedirectToAction(nameof(IndexAr));
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> LogInAr(SubmitLoginDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Error"] = "الرجاء ادخال البيانات صحيحة";
        //        return View();
        //    }
        //    if (!await _appUser.IsExist(x => x.Email == model.Email))
        //    {
        //        TempData["Error"] = "رقم الهاتف غير موجود";
        //        return View();
        //    }
        //    if (await _appUser.IsExist(x => x.Email == model.Email && x.IsDeleted))
        //    {
        //        TempData["Error"] = "تم حذف هذا المستخدم";
        //        return View();
        //    }
        //    if (await _appUser.IsExist(x => x.Email == model.Email && x.UserStatus == UserStatus.UserPending))
        //    {
        //        TempData["Error"] = "هذا المستخدم في انتظار الموافقة";
        //        return View();
        //    }
        //    if (await _appUser.IsExist(x => x.Email == model.Email && x.UserStatus == UserStatus.Reject))
        //    {
        //        TempData["Error"] = "هذا المستخدم مرفوض";
        //        return View();
        //    }
        //    var user = _appUser.Get(x => x.Email == model.Email).First();
        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var user1 = await _signInManager.PasswordSignInAsync(user.Email, model.Password, false, false);
        //        if (user1.Succeeded)
        //        {

        //            //var currentUser = _appUser.Get(x => x.PhoneNumber == user.PhoneNumber).First();


        //            //    var token = GenerateToken(currentUser);
        //            return RedirectToAction(nameof(IndexAr));


        //        }
        //    }
        //    TempData["Error"] = "الباسورد غير صحيح الرجاء المحاولة مره اخرى";

        //    return View();
        //}
        //public IActionResult RegisterAr()
        //{
        //    if (User.Identity.IsAuthenticated)
        //        return RedirectToAction(nameof(IndexAr));
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> RegisterAr(RegisterDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Error"] = "الرجاء ادخال البيانات صحيحة";
        //        return View();
        //    }
        //    if (await _appUser.IsExist(x => x.PhoneNumber == model.PhoneNumber))
        //    {
        //        TempData["Error"] = "هذا الهاتف موجود بالفعل";
        //        return View();
        //    }
        //    if (await _appUser.IsExist(x => x.Email == model.Email))
        //    {
        //        TempData["Error"] = "البريد الاكتروني  موجود بالفعل";
        //        return View();
        //    }
        //    try
        //    {
        //        //string Email = "User_" + RandomGenerator.GenerateString(4) + "@OceanHome.com";
        //        string Email = model.Email;

        //        /* string Email = "Admin"+ "@OceanHome.com";*/
        //        int verificationCode = RandomGenerator.GenerateNumber(1000, 9999);

        //        var user = new AppUser()
        //        {

        //            Email = Email,
        //            UserName = Email,
        //            Name = model.Name,
        //            PhoneNumber = model.PhoneNumber,
        //            VerificationCode = verificationCode,
        //            UserStatus = UserStatus.UserPending,
        //            PhoneNumberConfirmed = true,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //        };
        //        var result = await _userManager.CreateAsync(user, model.Password);
        //        if (!result.Succeeded)
        //        {
        //            TempData["Error"] = "الرجاء المحاولة في وقت لاحق";
        //            return View();
        //        }
        //        /* if (!await _roleManager.RoleExistsAsync("Admin"))
        //             await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //         if (!await _roleManager.RoleExistsAsync("User"))
        //             await _roleManager.CreateAsync(new IdentityRole("User"));*/
        //        await _userManager.AddToRoleAsync(user, "User");
        //        TempData["Success"] = "تم السجيل بنجاح";
        //        //send code to email
        //        string SMSMessage = $"رقم التفعيل الخاص بكم فى تطبيق إنفرا تركس هو {user.VerificationCode}";

        //        return RedirectToAction(nameof(LogInAr));
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = "الرجاء المحاولة في وقت لاحق";
        //        return View();
        //    }
        //}

        //[HttpPost]
        //public async Task<IActionResult> ForgetPasswordAr(string email)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Error"] = "Please Enter Data Correctely";
        //        return RedirectToAction(nameof(LogInAr));
        //    }
        //    if (email == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var user = _appUser.Get(x => x.Email == email && x.IsDeleted == false).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));

        //    }
        //    int NewPassword = RandomGenerator.GenerateNumber(100000, 999999);

        //    var newpass = _userManager.PasswordHasher.HashPassword(user, NewPassword.ToString());
        //    user.PasswordHash = newpass;
        //    await _userManager.UpdateAsync(user);
        //    TempData["success"] = "Password Changed Successfully";
        //    string SMSMessage = $"Your new password in the OceanHome application is {NewPassword}";

        //    return RedirectToAction(nameof(LogInAr));
        //}

        //public async Task<IActionResult> CourseDetailsAr(long Id)
        //{
        //    var UserId = _userManager.GetUserId(User);
        //    ViewBag.status = CourseUserStatus.not;
        //    if (UserId != null)
        //    {
        //        if (await _userCourse.IsExist(x => x.UserId == UserId && x.CourseId == Id && x.IsDeleted == false))
        //        {
        //            ViewBag.status = _userCourse.Get(x => x.UserId == UserId && x.CourseId == Id && x.IsDeleted == false).FirstOrDefault().Status;
        //        }
        //    }
        //    if (Id <= 0)
        //    {
        //        TempData["Error"] = "Some Thing Wrong";

        //        return RedirectToAction("IndexAr");
        //    }
        //    if (!await _course.IsExist(x => x.Id == Id))
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var course = _course.Get(x => x.Id == Id).FirstOrDefault();
        //    course.Instructor = _Instructor.Get(x => x.Id == course.InstructorId).FirstOrDefault();
        //    course.Images = _videos.Get(x => x.CourseId == Id && x.IsDeleted == false).ToList();
        //    return View(course);
        //}
        //[HttpDelete]
        //public async Task<IActionResult> PlayVideoAr(long videoId)
        //{
        //    var Userid = _userManager.GetUserId(User);
        //    var video = _videos.Get(x => (!x.IsDeleted) && x.Id == videoId).First();
        //    if (Userid == null && !video.Free)
        //    {
        //        TempData["Error"] = "User Not Authenticated";
        //        return RedirectToAction(nameof(IndexAr));
        //    }
        //    var publitio = new PublitioApi("fBN6XnD4X17baVGQLeQv", "xLgjeQzjyx4DzlnoM4sPRfBPnfk55rxQ");
        //    if (!await _videos.IsExist(x => x.Id == videoId && (!x.IsDeleted)))
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var keyValuePairs = await publitio.GetAsync("/files/show/kkk");
        //    if (video.CourseId == 10005)
        //    {
        //        publitio = new PublitioApi("DJpjgWgS2pwkzWvqLBbj", "Zx693HwoySzZk8enk3BucTPDh7a6X1Vw");
        //    }
        //    if (await _PublitioAccount.IsExist(x => x.Id == video.PublitioAccountID))
        //    {
        //        var account = _PublitioAccount.Get(x => x.Id == video.PublitioAccountID).First();
        //        publitio = new PublitioApi(account.key, account.secret);
        //    }

        //    string URL2 = null;
        //    if (video.Free == false && !await _userCourse.IsExist(x => x.UserId == Userid && x.CourseId == video.CourseId && x.Status == CourseUserStatus.Accepted))
        //    {

        //        return Json(new { success = false, message = "انت غير مشترك بهذا الدورة التدريبية" });

        //    }
        //    if (video.URL2 != null)
        //    {
        //        keyValuePairs = await publitio.GetAsync("/files/show/" + video.URL2);
        //        var success = keyValuePairs["success"].ToString();
        //        if (success == "True")
        //            URL2 = keyValuePairs["url_embed"].ToString();
        //    }
        //    if (URL2 == null)
        //    {
        //        TempData["Error"] = "Some This Wrong";
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    TempData["message"] = URL2;

        //    //return Redirect(URL2);
        //    return Json(new { success = true, message = URL2 });
        //}
        //public IActionResult ContactAr()
        //{
        //    IndexVm vm = new IndexVm();
        //    vm.information = _Information.Get(x => x.IsDeleted == false).First();
        //    return View(vm);
        //}
        //public IActionResult AboutAr()
        //{
        //    IndexVm vm = new IndexVm();
        //    vm.features = _feature.Get(x => x.IsDeleted == false).ToList();
        //    vm.aboutUS = _AboutUS.Get(x => x.IsDeleted == false).First();
        //    return View(vm);
        //}
        //public IActionResult ProfileAr()
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var user = _appUser.Get(x => x.Id == userId && x.IsDeleted == false).FirstOrDefault();
        //    if (user == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));

        //    }

        //    return View(user);
        //}
        //[HttpPost]
        //public async Task<IActionResult> EditProfileAr(string Name)
        //{
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var user = _appUser.Get(x => x.Id == userId && x.IsDeleted == false).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));

        //    }
        //    user.Name = Name;
        //    await _userManager.UpdateAsync(user);
        //    TempData["success"] = "تم التعديل بنجاح";

        //    return RedirectToAction(nameof(ProfileAr));
        //}
        //[HttpPost]
        //public async Task<IActionResult> verifyAr(string email, int first, int second, int third, int forth)
        //{

        //    var user = _appUser.Get(x => x.Email == email && x.IsDeleted == false).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var verifecationCode = first.ToString() + second.ToString() + third.ToString() + forth.ToString();
        //    if (user.VerificationCode.ToString() == verifecationCode)
        //    {
        //        user.UserStatus = UserStatus.Approved;
        //        await _userManager.UpdateAsync(user);
        //        TempData["success"] = "تم التفعيل بنجاح";
        //    }
        //    TempData["success"] = "لم يتم التفعيل ";
        //    return RedirectToAction(nameof(LogInAr));
        //}
        //[HttpPost]
        //public async Task<IActionResult> ChangePasswordAr(ChangePasswordDto model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["Error"] = "Please Enter Data Correctely";
        //        return RedirectToAction(nameof(ProfileAr));
        //    }
        //    var userId = _userManager.GetUserId(User);
        //    if (userId == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));
        //    }
        //    var user = _appUser.Get(x => x.Id == userId && x.IsDeleted == false).FirstOrDefault();

        //    if (user == null)
        //    {
        //        return RedirectToAction(nameof(NotFoundResult));

        //    }
        //    var check = await _userManager.CheckPasswordAsync(user, model.OldPassword);
        //    if (check == false)
        //    {
        //        TempData["Error"] = "كلمة المرور القديمة غير صحيحه";
        //        return RedirectToAction(nameof(ProfileAr));
        //    }
        //    var newpass = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
        //    user.PasswordHash = newpass;
        //    await _userManager.UpdateAsync(user);
        //    TempData["success"] = "تم تعديل كلمة المرور بنجاح";

        //    return RedirectToAction(nameof(ProfileAr));
        //}

        //public async Task<IActionResult> CoursesAr()
        //{
        //    var UserId = _userManager.GetUserId(User);
        //    IndexVm vm = new IndexVm();
        //    vm.Courses = _course.Get(x => x.IsDeleted == false && x.InstructorId != null && !x.Instructor.IsDeleted).ToList();
        //    foreach (var co in vm.Courses)
        //    {
        //        var instructor = _Instructor.Get(x => x.IsDeleted == false && x.Id == co.InstructorId).FirstOrDefault();
        //        co.Instructor = instructor;
        //        if (UserId != null && await _userCourse.IsExist(x => x.UserId == UserId && x.CourseId == co.Id && x.IsDeleted == false))
        //        {
        //            var userco = _userCourse.Get(x => x.IsDeleted == false && x.UserId == UserId && x.CourseId == co.Id).FirstOrDefault();


        //            switch (userco.Status)
        //            {
        //                case CourseUserStatus.Pending:
        //                    co.Sort = 0;
        //                    break;
        //                case CourseUserStatus.Accepted:
        //                    co.Sort = 1;
        //                    break;
        //                case CourseUserStatus.Rejected:
        //                    co.Sort = 2;
        //                    break;

        //            }
        //        }
        //        else
        //        {
        //            co.Sort = 10;
        //        }
        //        co.Images = _videos.Get(x => x.CourseId == co.Id && x.IsDeleted == false).ToList();
        //    }
        //    return View(vm);
        //}
        //public IActionResult TermsAr()
        //{
        //    IndexVm vm = new IndexVm();
        //    vm.TermsAndCondition = _TermsAndCondition.Get(x => x.IsDeleted == false).First();
        //    return View(vm);
        //}
        //[HttpDelete]
        //public async Task<IActionResult> SubscribeAr(long Id)
        //{
        //    if (!await _course.IsExist(x => x.Id == Id && (!x.IsDeleted)))
        //    {
        //        return Json(new { success = false, message = "الدورة التدريبية غير موجود" });

        //    }
        //    else
        //    {
        //        var userId = _userManager.GetUserId(User);
        //        await _userCourse.Add(new UserCourse()
        //        {
        //            CourseId = Id,
        //            UserId = userId
        //        });
        //        return Json(new { success = true, message = "تم تقديم طلبكم بنجاح الرجاء التواصل للوافقة" });

        //    }
        //}
        //public IActionResult NotFoundResultAr()
        //{
        //    return View();

        //}
        //public async Task<IActionResult> LogOutAr()
        //{

        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction(nameof(IndexAr));
        //}
        //#endregion


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult OldSite()
        {
            return View();
        }
        public IActionResult about()
        {
            return View();
        }
        public IActionResult projects()
        {
            return View();
        }
        public IActionResult projectDetails()
        {
            return View();
        }

    }
}
