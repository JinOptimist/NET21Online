using Microsoft.AspNetCore.Mvc;
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
    }
}
