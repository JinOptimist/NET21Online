using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class HelpfullController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
