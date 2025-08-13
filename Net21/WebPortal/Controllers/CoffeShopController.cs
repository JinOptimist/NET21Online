using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.Models.CoffeShop;

namespace WebPortal.Controllers
{
    public class CoffeShopController : Controller
    {
        //Delete it in next day
        private WebPortalContext _portalContext;

        public CoffeShopController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        public IActionResult Index()
        {
            var coffeProducts = _portalContext
                .CoffeeProducts
                .Select(dbCoffeProduct =>
                new CoffeeProductViewModel
                {
                    Id = dbCoffeProduct.Id,
                    Img = dbCoffeProduct.Img,
                    Name = dbCoffeProduct.Name,
                    Cell = dbCoffeProduct.Cell
                })
                .ToList();


            var userComments = _portalContext
                .UserComments
                .Select(dbUserComment =>
                new UserCommentViewModel
                {
                    Id = dbUserComment.Id,
                    ImgUser = dbUserComment.ImgUser,
                    NameUser = dbUserComment.NameUser,
                    Description = dbUserComment.Description,
                })
                .ToList();
            var model = new CoffeeShopViewModel
            {
                CoffeeProducts = coffeProducts,
                UserComments = userComments
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddCoffe()
        {
            return View();
        }

        // Remove Coffee
        public IActionResult RemoveCoffee(int id)
        {
            var coffee = _portalContext.CoffeeProducts.First(p => p.Id == id);
            _portalContext.CoffeeProducts.Remove(coffee);
            _portalContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // Remove Comments
        public IActionResult RemoveComment(int id)
        {
            var comment = _portalContext.UserComments.First(c => c.Id == id);
            _portalContext.UserComments.Remove(comment);
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
        public IActionResult AddComents(UserCommentViewModel viewUserComments)
        {
            var userCommentDB = new UserComment()
            {
                ImgUser = viewUserComments.ImgUser,
                NameUser = viewUserComments.NameUser,
                Description = viewUserComments.Description,
            };

            _portalContext.UserComments.Add(userCommentDB);
            _portalContext.SaveChanges();   

            return RedirectToAction("Index");
        }

        public IActionResult LoginPageTest()
        {
            return View();
        }
    }
}


