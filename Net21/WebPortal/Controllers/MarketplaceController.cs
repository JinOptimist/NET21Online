using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebPortal.Models.marketplace;

namespace WebPortal.Controllers
{
    public class MarketplaceController : Controller
    {
        // Заменить на бд 
        private static List<MarketplaceRegistrationViewModel> _users = new List<MarketplaceRegistrationViewModel>();

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(MarketplaceRegistrationViewModel model)
        {

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают");
                return View(model);
            }

            return RedirectToAction("Index");
        }

    }
}