using Ocean_Home.Helper;
using Ocean_Home.Interfaces;
using Ocean_Home.Models;
using Ocean_Home.Models.data;
using Ocean_Home.Models.Domain;
using Ocean_Home.Models.Enums;
using Ocean_Home.Models.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ocean_Home.Controllers
{
    public class CpController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManger;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IGeneric<AppUser> _user;

        public CpController(ILogger<HomeController> logger, UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManger,
            IConfiguration configuration, IGeneric<AppUser> user)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManger = signInManger;
            _configuration = configuration;
            _user = user;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction(nameof(Login));

            }
            if (!User.IsInRole("Admin"))
            {
                return RedirectToAction("Login", "cp");
            }

            if (string.IsNullOrEmpty(User.Identity.Name))
                return RedirectToAction(nameof(Login));

            return View();
        }
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return RedirectToAction(nameof(Index));

            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            string Email = "Admin@OceanHome.Com";
            /* string Email = "Admin"+ "@OceanHome.com";*/
            int verificationCode = RandomGenerator.GenerateNumber(1000, 9999);
            var user = new AppUser()
            {
                Email = Email,
                UserName = "Admin@OceanHome.Com",
                Name = "Admin",
                PhoneNumber = "000000000",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(user, "123456");
            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, "Admin");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _signInManger.PasswordSignInAsync(Model.Email, Model.Password, false, false);
            if (user.Succeeded)
            {

                var currentUser = _user.Get(x => x.Email == Model.Email).First();

                if (await _userManager.IsInRoleAsync(currentUser, "Admin"))
                {
                    var token = GenerateToken(currentUser);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await _signInManger.SignOutAsync();
                    ModelState.AddModelError("", "غير مسموح لك بالدخول");
                    return View(Model);
                }
            }
            ModelState.AddModelError("", "محاولة دخول خاطئه");
            return View(Model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManger.SignOutAsync();

            return RedirectToAction("Login", "Cp");
        }
        public async Task<string> GenerateToken(AppUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddYears(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
