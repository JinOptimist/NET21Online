using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
        
        public IActionResult SdekProject()
        {
            return View(); // Ищет Views/Home/SdekProject.cshtml
        }
    }
}
