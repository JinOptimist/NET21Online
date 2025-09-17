using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
