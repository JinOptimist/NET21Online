using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.CoffeShop;

namespace WebPortal.Controllers
{
    public class CoffeShopController : Controller
    {

        private ICoffeeProductRepository _productRepository;
        private IUserCommentRepository _commentRepository;

        public CoffeShopController(ICoffeeProductRepository productRepository, IUserCommentRepository commentRepository)
        {
            _productRepository = productRepository;
            _commentRepository = commentRepository;
        }

        public IActionResult Index()
        {
            var coffeProducts = _productRepository
                .GetAll()
                .Select(dbCoffeProduct =>
                new CoffeeProductViewModel
                {
                    Id = dbCoffeProduct.Id,
                    Img = dbCoffeProduct.Img,
                    Name = dbCoffeProduct.Name,
                    Cell = dbCoffeProduct.Cell
                })
                .ToList();


            var userComments = _commentRepository
                .GetAll()
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
            _productRepository.Remove(id);
            return RedirectToAction("Index");
        }

        // Remove Comments
        public IActionResult RemoveComment(int id)
        {
            _commentRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Products()
        {
            var modelProduct = new CoffeeShopViewModel
            {
                CoffeeProducts = _productRepository
                .GetAll()
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
                UserComments = _commentRepository
                .GetAll()
                .Select(db => new UserCommentViewModel
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
            //if (viewcoffe == null)
            //{
            //    return BadRequest("No coffee data provided.");
            //}

            //if (string.IsNullOrWhiteSpace(viewcoffe.Name) ||
            //    string.IsNullOrWhiteSpace(viewcoffe.Img) ||
            //    viewcoffe.Cell <= 0)
            //{
            //    ModelState.AddModelError("", "Please fill all required fields.");
            //    return View(viewcoffe);
            //}
            if (!ModelState.IsValid)
            {
                return View(viewcoffe);
            }

            var coffeDB = new CoffeeProduct()
            {
                Img = viewcoffe.Img,
                Name = viewcoffe.Name,
                Cell = viewcoffe.Cell
            };

            _productRepository.Add(coffeDB);
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

            _commentRepository.Add(userCommentDB);
            return RedirectToAction("Index");
        }

        public IActionResult LoginPageTest()
        {
            return View();
        }
    }
}


