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
            if (coffee == null)
            {
                return NotFound($"Coffee with id={id} not found.");
            }
            _portalContext.CoffeeProducts.Remove(coffee);
            _portalContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // Remove Comments
        public IActionResult RemoveComment(int id)
        {
            var comment = _portalContext.UserComments.First(c => c.Id == id);
            if (comment == null)
            {
                return NotFound($"Comment with id={id} not found.");
            }
            _portalContext.UserComments.Remove(comment);
            _portalContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Products()
        {
            var modelProduct = new CoffeeShopViewModel
            {
                CoffeeProducts = _portalContext.CoffeeProducts
                    .Select(db => new CoffeeProductViewModel
                    {
                        Id = db.Id,
                        Img = db.Img,
                        Name = db.Name,
                        Cell = db.Cell
                    })
                    .ToList(),
            };

            return View(modelProduct);
        }

        [HttpGet]
        public IActionResult CommentsUsers()
        {
            var model = new CoffeeShopViewModel
            { 
                UserComments = _portalContext.UserComments
                .Select(db=>new UserCommentViewModel
                { 
                    Id = db.Id,
                    ImgUser = db.ImgUser,
                    NameUser = db.NameUser,
                    Description = db.Description
                
                })
                .ToList()
            };

            return View(model);
        }



        [HttpPost]
        public IActionResult AddCoffe(CoffeeProductViewModel viewcoffe)
        {
            if (viewcoffe == null)
            {
                return BadRequest("No coffee data provided.");
            }

            if (string.IsNullOrWhiteSpace(viewcoffe.Name) ||
                string.IsNullOrWhiteSpace(viewcoffe.Img) ||
                viewcoffe.Cell <= 0)
            {
                ModelState.AddModelError("", "Please fill all required fields.");
                return View(viewcoffe);
            }

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
            if (viewUserComments == null)
            {
                return BadRequest("No comments data provided.");
            }

            if (string.IsNullOrWhiteSpace(viewUserComments.NameUser) ||
                string.IsNullOrWhiteSpace(viewUserComments.Description))
            {
                ModelState.AddModelError("", "Please fill all required fields.");
                return View(viewUserComments);
            }

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


