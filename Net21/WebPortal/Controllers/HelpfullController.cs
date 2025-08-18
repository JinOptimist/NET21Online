using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;

namespace WebPortal.Controllers
{
    public class HelpfullController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Csharp()
        {
            return View();
        }

        private WebPortalContext _context;

        public HelpfullController(WebPortalContext context) 
        {
            _context = context;
        }
    }
}
