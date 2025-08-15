using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
