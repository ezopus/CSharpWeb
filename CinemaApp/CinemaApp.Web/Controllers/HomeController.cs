
namespace CinemaApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // two ways to pass data to the view
            // 1. viewdata/viewbag
            // 2. pass ViewModel to View
            ViewData["MovieTitle"] = "Home Page";
            ViewData["Message"] = "Welcome to our Cinema Web App!";
            return View();
        }

    }
}
