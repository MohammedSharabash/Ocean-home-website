using Microsoft.AspNetCore.Mvc;
using Ocean_Home.Models;
using System.Diagnostics;

namespace Ocean_Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

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
