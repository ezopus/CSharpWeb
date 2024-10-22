using Microsoft.AspNetCore.Mvc;

namespace StorageLocker.Web.Controllers
{
    public class LockerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
