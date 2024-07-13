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
        private readonly IGeneric<Project> _Project;
        private readonly IGeneric<JobsDepartment> _JobsDepartments;
        private readonly IGeneric<ProjectsDepartment> _ProjectsDepartments;
        private readonly IGeneric<Slider> _Slider;
        private readonly IGeneric<Specialty> _Specialty;
        private readonly IGeneric<Manager> _Manager;
        private readonly IGeneric<OurAffiliate> _OurAffiliates;
        private readonly IGeneric<AboutUS> _AboutUS;
        private readonly IGeneric<Employee> _Employee;
        private readonly IGeneric<ContactUS> _ContactUS;
        private readonly IGeneric<ProjectImage> _ProjectImages;
        private readonly IGeneric<AppUser> _appUser;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(IGeneric<Project> Project, IGeneric<JobsDepartment> JobsDepartment, IGeneric<ProjectsDepartment> ProjectsDepartment, IGeneric<OurAffiliate> OurAffiliates, IGeneric<Employee> employee, IGeneric<Specialty> Specialty, IGeneric<Slider> Slider, IGeneric<AboutUS> AboutUS, IGeneric<ContactUS> ContactuS, IGeneric<Manager> manager,
          IUserProject userProjectService, IConfiguration configuration, IGeneric<ProjectImage> Images, IGeneric<AppUser> AppUser, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _Project = Project;
            _Manager = manager;
            _Slider = Slider;
            _Specialty = Specialty;
            _AboutUS = AboutUS;
            _Employee = employee;
            _ContactUS = ContactuS;
            _OurAffiliates = OurAffiliates;
            _ProjectImages = Images;
            _ProjectsDepartments = ProjectsDepartment;
            _JobsDepartments = JobsDepartment;
            _appUser = AppUser;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            var UserId = _userManager.GetUserId(User);
            var vm = GetData();
            return View(vm);
        }
        public IActionResult OldSite()
        {
            var vm = GetData();
            return View(vm);
        }

        public IActionResult about()
        {
            var vm = new IndexVm();
            vm.aboutUS = _AboutUS.Get(x => x.IsDeleted == false).First();
            vm.Employees = _Employee.Get(x => x.IsDeleted == false && x.DepartmentId > 0 && !x.Department.IsDeleted).ToList();
            vm.JobsDepartments = _JobsDepartments.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.Managers = _Manager.Get(x => x.IsDeleted == false).ToList();
            vm.contact = _ContactUS.Get(x => x.IsDeleted == false).First();

            return View(vm);
        }
        public IActionResult contactus()
        {
            var vm = new IndexVm();
            vm.aboutUS = _AboutUS.Get(x => x.IsDeleted == false).First();
            vm.Employees = _Employee.Get(x => x.IsDeleted == false && x.DepartmentId > 0 && !x.Department.IsDeleted).ToList();
            vm.JobsDepartments = _JobsDepartments.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.Managers = _Manager.Get(x => x.IsDeleted == false).ToList();
            vm.contact = _ContactUS.Get(x => x.IsDeleted == false).First();

            return View(vm);
        }
        public IActionResult downloadpdf()
        {
            var vm = new IndexVm();
            vm.aboutUS = _AboutUS.Get(x => x.IsDeleted == false).First();
            vm.Employees = _Employee.Get(x => x.IsDeleted == false && x.DepartmentId > 0 && !x.Department.IsDeleted).ToList();
            vm.JobsDepartments = _JobsDepartments.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.Managers = _Manager.Get(x => x.IsDeleted == false).ToList();
            vm.contact = _ContactUS.Get(x => x.IsDeleted == false).First();

            return View(vm);
        }
        public IActionResult projects()
        {
            var vm = GetData();
            return View(vm);
        }
        public IActionResult projectDetails(long id)
        {
            var vm = new IndexVm();
            var project = _Project.Get(x => x.Id == id).First();
            var images = _ProjectImages.Get(x => x.IsDeleted == false && x.ProjectId == id).OrderBy(x => x.Sort).ToList();
            vm.Projects.Add(project);
            vm.ProjectImages = images;
            vm.contact = _ContactUS.Get(x => x.IsDeleted == false).First();
            return View(vm);
        }
        private IndexVm GetData()
        {
            IndexVm vm = new IndexVm();
            vm.Projects = _Project.Get(x => x.IsDeleted == false && x.DepartmentId > 0 && !x.Department.IsDeleted).OrderBy(x => x.Sort).ToList();
            vm.ProjectsDepartments = _ProjectsDepartments.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.JobsDepartments = _JobsDepartments.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.ProjectImages = _ProjectImages.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.Sliders = _Slider.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.Specialties = _Specialty.Get(x => x.IsDeleted == false).OrderBy(x => x.Sort).ToList();
            vm.Managers = _Manager.Get(x => x.IsDeleted == false).ToList();
            vm.Employees = _Employee.Get(x => x.IsDeleted == false && x.DepartmentId > 0 && !x.Department.IsDeleted).ToList();
            vm.OurAffiliates = _OurAffiliates.Get(x => x.IsDeleted == false).ToList();
            vm.aboutUS = _AboutUS.Get(x => x.IsDeleted == false).First();
            vm.contact = _ContactUS.Get(x => x.IsDeleted == false).First();
            return vm;
        }

    }
}
