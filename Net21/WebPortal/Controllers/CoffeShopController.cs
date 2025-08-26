using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.CoffeShop;

namespace WebPortal.Controllers
{
    public class CoffeShopController : Controller
    {
        private ICoffeeProductRepository _productRepository;
        private IUserCommentRepository _commentRepository;
        private IUserRepositrory _userRepository;
        //private IUserCoffeShopRepository _userRepository;

        public CoffeShopController(
            ICoffeeProductRepository productRepository,
            IUserCommentRepository commentRepository,
            IUserRepositrory userRepository)
        {
            _productRepository = productRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var coffeProducts = _productRepository
                .GetAllWithAuthors()
                .Select(db => new CoffeeProductViewModel
                {
                    Id = db.Id,
                    Img = db.Img,
                    Name = db.Name,
                    Cell = db.Cell,
                    AuthorName = db.AuthorAdd != null ? db.AuthorAdd.UserName : "Unknown"
                })
                .ToList();

            var userComments = _commentRepository
                .GetAll()
                .Select(db => new UserCommentViewModel
                {
                    Id = db.Id,
                    ImgUser = db.ImgUser,
                    NameUser = db.NameUser,
                    Description = db.Description,
                })
                .ToList();

            var model = new CoffeeShopViewModel
            {
                CoffeeProducts = coffeProducts,
                UserComments = userComments
            };

            return View(model);
        }

        // ------------------ COFFEE ------------------

        [HttpGet]
        public IActionResult AddCoffe()
        {
            var model = new CoffeeProductViewModel
            {
                AvailableAuthors = _userRepository.GetAll()
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.UserName,
                    })
                    .ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddCoffe(CoffeeProductViewModel viewcoffe)
        {
            //if (!ModelState.IsValid)
            //{
            //    viewcoffe.AvailableAuthors = _userRepository.GetAll()
            //        .Select(u => new SelectListItem
            //        {
            //            Value = u.Id.ToString(),
            //            Text = u.UserName
            //        })
            //        .ToList();
            //    return View(viewcoffe);
            //}

            try
            {
                var coffeDB = new CoffeeProduct
                {
                    Img = viewcoffe.Img,
                    Name = viewcoffe.Name,
                    Cell = viewcoffe.Cell,
                    AuthorId = viewcoffe.AuthorId
                };

                _productRepository.Add(coffeDB);
                return RedirectToAction("Products");             
            }
            catch 
            {
                return View(viewcoffe);
            }
        }

        [HttpGet]
        public IActionResult EditCoffe(int id)
        {
            var coffee = _productRepository.GetFirstById(id);
            if (coffee == null)
            { 
                return NotFound();
            }

            var model = new CoffeeProductViewModel
            {
                Id = coffee.Id,
                Img = coffee.Img,
                Name = coffee.Name,
                Cell = coffee.Cell,
                AuthorId = coffee.AuthorId,
                AvailableAuthors = _userRepository.GetAll()
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.UserName
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditCoffe(CoffeeProductViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.AvailableAuthors = _userRepository.GetAll()
            //        .Select(u => new SelectListItem
            //        {
            //            Value = u.Id.ToString(),
            //            Text = u.UserName,
            //        })
            //        .ToList();
            //    return View(model);
            //}

            var coffee = _productRepository.GetFirstById(model.Id);
            if (coffee == null)
            { 
                return NotFound();
            }

            coffee.Img = model.Img;
            coffee.Name = model.Name;
            coffee.Cell = model.Cell;
            coffee.AuthorId = model.AuthorId;

            _productRepository.Update(coffee);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCoffee(int id)
        {
            _productRepository.Remove(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Products()
        {
            var model = new CoffeeShopViewModel
            {
                CoffeeProducts = _productRepository
                    .GetAllWithAuthors()
                    .Select(db => new CoffeeProductViewModel
                    {
                        Id = db.Id,
                        Img = db.Img,
                        Name = db.Name,
                        Cell = db.Cell,
                        AuthorName = db.AuthorAdd != null ? db.AuthorAdd.UserName : "Unknown"
                    })
                    .ToList()
            };

            return View(model);
        }

        // ------------------ COMMENTS ------------------

        [HttpGet]
        public IActionResult AddComents()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddComents(UserCommentViewModel viewUserComments)
        {
            if (!ModelState.IsValid)
            { 
                return View(viewUserComments);
            }

            var userCommentDB = new UserComment
            {
                ImgUser = viewUserComments.ImgUser,
                NameUser = viewUserComments.NameUser,
                Description = viewUserComments.Description,
            };

            _commentRepository.Add(userCommentDB);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveComment(int id)
        {
            _commentRepository.Remove(id);
            return RedirectToAction("Index");
        }
        //----------------USERS---------------------

        public IActionResult LoginPageTest()
        {
            return View();
        }
    }
}
