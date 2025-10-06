using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;
using System.Reflection;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;
using WebPortal.Hubs;
using WebPortal.Models.CoffeShop;
using WebPortal.Models.Home;
using WebPortal.Services;
using WebPortal.Services.Permissions.CoffeShop;

namespace WebPortal.Controllers
{
    [Authorize]
    public class CoffeShopController : Controller
    {
        private ICoffeeProductRepository _productRepository;
        private IUserCommentRepository _commentRepository;
        private IUserRepositrory _userRepository;
        private IAuthService _authService;
        private ICoffeShopPermision _coffeShopPermision;
        private ICoffeShopFileServices _coffeShopFileServices;
        private IHubContext<NotificationHubCoffeShop, INotificationHubCoffeShop> _hubContextCoffe;


        public CoffeShopController(
            ICoffeeProductRepository productRepository,
            IUserCommentRepository commentRepository,
            IUserRepositrory userRepository,
            IAuthService authService,
            ICoffeShopPermision coffeShopPermision,
            ICoffeShopFileServices coffeShopFileServices,
            IHubContext<NotificationHubCoffeShop, INotificationHubCoffeShop> hubContextCoffe)
        {
            _productRepository = productRepository;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _authService = authService;
            _coffeShopPermision = coffeShopPermision;
            _coffeShopFileServices = coffeShopFileServices;
            _hubContextCoffe = hubContextCoffe;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var layoutModel = new HomeCoffeShopViewModel
            {
                Id = _authService.IsAuthenticated() ? _authService.GetId() : 0,
                UserName = _authService.IsAuthenticated() ? _authService.GetUser().UserName : "Guest",
                ImageFon = _coffeShopFileServices.GetFonGallery()
            };

            var currentUserId = _authService.IsAuthenticated()
               ? _authService.GetId()
               : 0;

            var coffeProducts = _productRepository
                .GetAllWithAuthors()
                .Select(db => new CoffeeProductViewModel
                {
                    Id = db.Id,
                    Img = db.Img,
                    Name = db.Name,
                    Cell = db.Cell,
                    AuthorName = db.AuthorAdd != null ? db.AuthorAdd.UserName : "Unknown",
                    CanFindPage = _coffeShopPermision.CanFindPage(db)
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
                LayoutModelUser = layoutModel,
                CoffeeProducts = coffeProducts,
                UserComments = userComments
            };

            return View(model);
        }

        // ------------------ COFFEE ------------------

        [HttpGet]
        [Role(Role.CoffeProductModerator)]
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
        [Role(Role.CoffeProductModerator)]
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
            var currentUserId = _authService.IsAuthenticated()
               ? _authService.GetId()
               : 0;

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
                        AuthorName = db.AuthorAdd != null ? db.AuthorAdd.UserName : "Unknown",
                        CanFindPage = _coffeShopPermision.CanFindPage(db)
                    })
                    .ToList()
            };

            return View(model);
        }

        // ------------------ COMMENTS ------------------

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

        [Role(Role.CoffeProductModerator)]
        public IActionResult UpdateImagePage()
        {
            return View();
        }

        [HttpPost]
        [Role(Role.CoffeProductModerator)]
        public IActionResult UpdateImagePage(IFormFile pageimage)
        {
            _coffeShopFileServices.UploudFonCoffeShop(pageimage);
            return RedirectToAction("Index");
        }

        [Role(Role.CoffeProductModerator)]
        public IActionResult ManageGallery()
        {
            var model = new CoffeeProductViewModel
            {
                GalleryImages = _coffeShopFileServices.GetFonGallery()
            };

            return View(model);
        }

        [HttpPost]
        [Role(Role.CoffeProductModerator)]
        public IActionResult RemoveImage(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                _coffeShopFileServices.RemoveImageSlider(fileName);
            }
            return RedirectToAction("ManageGallery");
        }

        [Role(Role.CoffeProductModerator)]
        public IActionResult CoffeStatistics()
        {
            var coffeeDetails = _productRepository.GetCoffeeDetail();
            var coffeeSumary = _productRepository.GetCoffeeSummary();
            var model = new CoffeeStatisticsViewModel
            {
                CoffeeDetails = coffeeDetails,
                CoffeeSummary = coffeeSumary
            };
            return View(model);
        }

        [HttpPost]
        [Role(Role.CoffeProductModerator)]
        public IActionResult EditCoffeName(int id, string name)
        {

            var user = _authService.GetUser();


            var coffeProduct = _productRepository.GetFirstById(id);

            if (coffeProduct.AuthorAdd != user)
            {
                return Json(false);
            }

            coffeProduct.Name = name;
            _productRepository.Update(coffeProduct);

            return Json(true);
        }

        [HttpPost]
        [Role(Role.CoffeProductModerator)]
        public IActionResult DeleteCoffee(int id)
        {
            _productRepository.Remove(id);
            return Json(new { success = true });
        }

        [HttpGet]
        [Role(Role.CoffeProductModerator)]
        public IActionResult SendNotificationPage()
        {
            return View();
        }

        [HttpPost]
        [Role(Role.CoffeProductModerator)]
        public IActionResult SendNotification([FromForm] string message)
        {
            _hubContextCoffe.Clients.All.NewNotificationCoffeShop(message).Wait();
            return Json(true);
        }


        public IActionResult CheckGuestEndPointsCoffe()
        {
            var controllerTypeCoffee = typeof(CoffeShopController);

            //Getting all the public methods that return IActionResult
            var action = controllerTypeCoffee
                .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                .Where(methods => typeof(IActionResult).IsAssignableFrom(methods.ReturnType));


            var viewModelCoffee = action
            .Select(methods =>
            {
                var httpAttribute = methods.GetCustomAttributes()
                 .OfType<HttpMethodAttribute>()
                 .FirstOrDefault();

                var routeAttr = methods.GetCustomAttribute<RouteAttribute>();

                var filters = methods.GetCustomAttributes()
                .Where(a => typeof(ActionFilterAttribute).IsAssignableFrom(a.GetType()))
                .Select(a => a.GetType().Name)
                .ToList();

                var parameters = methods.GetParameters()
                .Select(p => $"{p.ParameterType.Name} {p.Name}")
                .ToList();

                return new EndPointsCoffeViewModel
                {
                    ControllerName = controllerTypeCoffee.Name,
                    ActionName = methods.Name,
                    HttpVerb = httpAttribute?.HttpMethods.FirstOrDefault() ?? "GET",
                    Route = routeAttr?.Template ?? "",
                    ViewModelTypeName = methods.GetParameters().FirstOrDefault()?.ParameterType.Name ?? "No Parameters",
                    HasAuthorize = methods.GetCustomAttribute<AuthorizeAttribute>() != null,
                    HasAllowAnonymous = methods.GetCustomAttribute<AllowAnonymousAttribute>() != null,
                    Parameters = parameters,
                    Filters = filters
                };
            }).ToList();

            return View(viewModelCoffee);
        }

        //[HttpPost]
        //[Role(Role.CoffeProductModerator)]
        //public async Task<IActionResult> SendNotification([FromForm] string message)
        //{
        //    if (string.IsNullOrWhiteSpace(message))
        //    {
        //        return BadRequest("Message cannot be empty.");
        //    }

        //    await _hubContextCoffe.Clients.All.NewNotificationCoffeShop(message);
        //    return Ok(new { success = true });
        //}

    }
}
