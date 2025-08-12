using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.Models;
using WebPortal.Models.marketplace;

namespace WebPortal.Controllers
{
    public class MarketplaceController : Controller
    {
        private WebPortalContext _portalContext;

        public MarketplaceController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }
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
            var userDb = new MarketplaceUser()
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                ConfirmPassword = model.ConfirmPassword,
            };

            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают");
                return View(model);
            }
            else 
            {
                _portalContext.MarketplaceUsers.Add(userDb);
                _portalContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SingIn(MarketplaceRegistrationViewModel model)
        {
            // Проверяем модель
            if (!ModelState.IsValid)
            {
                return View("SingIn", model); // Возвращаем представление с ошибками
            }

            // Поиск пользователя по email
            var user = _portalContext.MarketplaceUsers
                .FirstOrDefault(u => u.Email == model.Email);

            if (user == null || user.Password != model.Password)
            {
                ModelState.AddModelError("", "Неверный email или пароль"); // Добавляем ошибку
                return View("SingIn", model); // Возвращаем представление с ошибками
            }

            // Успешный вход
            return RedirectToAction("Index");
        }

    }
}