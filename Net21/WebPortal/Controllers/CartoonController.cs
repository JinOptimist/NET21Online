using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class CArtoonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}