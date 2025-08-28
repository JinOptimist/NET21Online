using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.Users;

namespace WebPortal.Controllers
{
    public class UserCoffeShopController : Controller
    {
        private IUserCoffeShopRepository _userCoffeShopRepository;

        public UserCoffeShopController(IUserCoffeShopRepository userCoffeShopRepository)
        { 
            _userCoffeShopRepository = userCoffeShopRepository;
        }

        public IActionResult Users()
        { 
            var model = _userCoffeShopRepository.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateUser() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            _userCoffeShopRepository.Add(user);
            return RedirectToAction("Users");

        }

        [HttpGet]
        public IActionResult EditUser(int id)
        { 
            var user = _userCoffeShopRepository.GetFirstById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        { 
            _userCoffeShopRepository.Update(user);
            return RedirectToAction("Users");
        }

        public IActionResult RemoveUser(int id)
        {
            _userCoffeShopRepository.Remove(id);
            return RedirectToAction("Users");
        }
    }
}
