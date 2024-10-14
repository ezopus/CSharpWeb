using Microsoft.AspNetCore.Mvc;
using SeminarHub.Models;
using System.Diagnostics;

namespace SeminarHub.Controllers
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
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("All", "Seminar");
            }

            return View();
        }
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated != null)
            {
                return View(nameof(Index));
            }
            else
            {
                return RedirectToPage("Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}