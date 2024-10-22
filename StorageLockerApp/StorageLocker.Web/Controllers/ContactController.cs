using Microsoft.AspNetCore.Mvc;

namespace StorageLocker.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
