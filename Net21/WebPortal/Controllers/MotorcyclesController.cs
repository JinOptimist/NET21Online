using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class MotorcyclesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
