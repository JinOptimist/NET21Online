using Microsoft.AspNetCore.Mvc;

namespace WebPortal.Controllers
{
    public class MarketplaceController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
