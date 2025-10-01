using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebPortal.Models;
using WebPortal.Models.Home;
using WebPortal.Services;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private IAuthService _authService;

        public HomeController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();

            if (_authService.IsAuthenticated())
            {
                var id = _authService.GetId();
                var name = _authService.GetUser().UserName;

                viewModel.Id = id;
                viewModel.Name = name;
            }
            else
            {
                viewModel.Id = 0;
                viewModel.Name = "guess";
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Smile(int age, string name, bool isRed)
        {
            var yearOfBirthday = DateTime.Now.Year - age;

            var viewModel = new SmileViewModel();
            viewModel.Name = name +
                (isRed ? " soulless" : "");
            viewModel.YearOfBirthday = yearOfBirthday;
            return View(viewModel);
        }

        public IActionResult CallRequest()
        {
            return View(); // Ищет Views/Home/CallRequest.cshtml
        }

        public IActionResult CheckGuestEndpoints()
        {
            var contollers = Assembly
                .GetAssembly(typeof(HomeController))!
                .GetTypes()
                .Where(type => type.BaseType == typeof(Controller));

            var viewModelsMain = new List<EndPointViewModel>();
            foreach (var contoller in contollers)
            {
                var isAutorizeOnContollerLevel = contoller
                    .GetCustomAttribute<AuthorizeAttribute>() != null;

                var actions = contoller
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(method => method.ReturnType == typeof(IActionResult))
                    .Where(method =>
                        (!isAutorizeOnContollerLevel
                            && method.GetCustomAttribute<AuthorizeAttribute>() == null
                        || isAutorizeOnContollerLevel
                            && method.GetCustomAttribute<AllowAnonymousAttribute>() != null)

                        && method.GetCustomAttribute<HttpPostAttribute>() != null);

                var viewModels = actions
                    .Select(method =>
                        new EndPointViewModel
                        {
                            ActionName = method.Name,
                            ContollerName = contoller.Name,
                            ViewModelTypeName = method.GetParameters().FirstOrDefault()?.ParameterType.Name ?? "NO PARAMETERS",
                        })
                    .ToList();
                viewModelsMain.AddRange(viewModels);
            }

            return View(viewModelsMain);
        }
    }
}
