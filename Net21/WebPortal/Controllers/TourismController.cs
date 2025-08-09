using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class TourismController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
