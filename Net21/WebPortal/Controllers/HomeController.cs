using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebPortal.Models;
using WebPortal.Models.Home;
using WebPortal.Services;
using WebPortal.Services.Apis;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        private IAuthService _authService;
        private SuperService _superService;
        private WeatherApi _weatherApi;
        private JokeApi _jokeApi;

        public HomeController(IAuthService authService, SuperService superService, WeatherApi weatherApi, JokeApi jokeApi)
        {
            _authService = authService;
            _superService = superService;
            _weatherApi = weatherApi;
            _jokeApi = jokeApi;
        }

        public async Task<IActionResult> Index()
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

            // Получаем две случайные шутки
            try
            {
                var jokes = await _jokeApi.GetTwoRandomJokes();
                viewModel.Jokes = jokes.Select(j => new JokeViewModel
                {
                    Type = j.type,
                    Setup = j.setup,
                    Punchline = j.punchline,
                    Id = j.id
                }).ToList();
            }
            catch (Exception ex)
            {
                // В случае ошибки API, оставляем пустой список шуток
                viewModel.Jokes = new List<JokeViewModel>();
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

        [HttpGet]
        public async Task<IActionResult> GetWeather(double latitude, double longitude)
        {
            try
            {
                var weather = await _weatherApi.GetCurrentWeather(latitude, longitude);
                return Json(weather);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Не удалось получить данные о погоде" });
            }
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
