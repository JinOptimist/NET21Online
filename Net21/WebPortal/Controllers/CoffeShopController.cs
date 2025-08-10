using Microsoft.AspNetCore.Mvc;
using WebPortal.Models.CoffeShop;

namespace WebPortal.Controllers
{
    public class CoffeShopController : Controller
    {
        //Delete it in next day
        private static List<CoffeeProduct> CoffeProd = new List<CoffeeProduct>();
        private static List<UserComment> UserCom = new List<UserComment>();


        public IActionResult Index()
        {
            if (!CoffeProd.Any())
            {
                string[] names = { "Espresso", "Latte", "Arabic coffee", "Cappuccino", "Mocha", "Flat White" };
                int[] cell = { 35, 44, 55, 25, 60, 15 };
                for (int i = 0; i < names.Length; i++)
                {
                    var coffee = new CoffeeProduct
                    {
                        Img = $"/images/coffeshop/p{i + 1}.png",
                        Name = names[i],
                        Cell = cell[i]
                    };

                    CoffeProd.Add(coffee);
                }
            }

            if (!UserCom.Any())
            {
                string[] userName = { "Name1", "Name2", "Name3" };
                string[] description = { "Nice Coffe", "Top Coffe", "I love tropical coffe" };
                for (int i = 0; i < userName.Length; i++)
                {
                    var userProfile = new UserComment
                    {
                        ImgUser = $"/images/coffeshop/rev{i + 1}.jpg",
                        NameUser = userName[i],
                        Description = description[i]
                    };

                    UserCom.Add(userProfile);
                }

            }
            var model = new CoffeeShopViewModel
            {
                CoffeeProducts = CoffeProd,
                UserComments = UserCom
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddCoffe()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCoffe(CoffeeProduct viewcoffe)
        {
            CoffeProd.Add(viewcoffe);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddComents()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComents(UserComment userComments)
        {
            UserCom.Add(userComments);
            return RedirectToAction("Index");
        }

        public IActionResult LoginPageTest()
        {
            return View();
        }
    }
}


