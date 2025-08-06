using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class CoffeShopController : Controller
    {
        public IActionResult Index()
        {
            var model = new CoffeeShopViewModel
            {
                CoffeeProducts = new List<string>
                {
                    "images/coffeshop/p1.png",
                    "images/coffeshop/p2.png",
                    "images/coffeshop/p3.png",
                    "images/coffeshop/p4.png",
                    "images/coffeshop/p5.png",
                    "images/coffeshop/p6.png"
                },
                UserComments = new List<string>
                {
                    "images/coffeshop/rev1.jpg",
                    "images/coffeshop/rev2.jpg",
                    "images/coffeshop/rev3.jpg"
                }
            };

            return View(model);  
        }

    }
}


