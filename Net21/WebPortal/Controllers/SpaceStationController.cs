using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class SpaceStationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
