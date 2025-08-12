using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.Models.CoffeShop;

namespace WebPortal.Controllers
{
    public class CoffeShopController : Controller
    {
        //Delete it in next day
        private static List<UserCommentViewModel> UserCom = new List<UserCommentViewModel>();
        private WebPortalContext _portalContext;

        public CoffeShopController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        public IActionResult Index()
        {
            var coffeProducts = _portalContext.
                CoffeeProducts
                //.OrderBy(x => x.Cell)
                .Select(dbCoffeProduct => 
                new CoffeeProductViewModel
                {   
                    Id = dbCoffeProduct.Id,
                    Img = dbCoffeProduct.Img,
                    Name = dbCoffeProduct.Name,
                    Cell = dbCoffeProduct.Cell
                })
                .ToList();
            //Tommorow edit and adding save in DB
            if (!UserCom.Any())
            {
                string[] userName = { "Name1", "Name2", "Name3" };
                string[] description = { "Nice Coffe", "Top Coffe", "I love tropical coffe" };
                for (int i = 0; i < userName.Length; i++)
                {
                    var userProfile = new UserCommentViewModel
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
                CoffeeProducts = coffeProducts,
                UserComments = UserCom
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddCoffe()
        {
            return View();
        }
        public IActionResult Remove(int Id)
        {
            var coffeRemoveToDB=_portalContext.CoffeeProducts.First(p => p.Id == Id);
            _portalContext.CoffeeProducts.Remove(coffeRemoveToDB);
            _portalContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Products()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddCoffe(CoffeeProductViewModel viewcoffe)
        {
            var coffeDB = new CoffeeProduct()
            {
                Img = viewcoffe.Img,
                Name = viewcoffe.Name,
                Cell = viewcoffe.Cell
            };

            _portalContext.CoffeeProducts.Add(coffeDB);
            _portalContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddComents()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddComents(UserCommentViewModel userComments)
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


