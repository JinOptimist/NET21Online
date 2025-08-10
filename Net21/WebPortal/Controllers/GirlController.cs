using Microsoft.AspNetCore.Mvc;
using System.IO.Pipes;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class GirlController : Controller
    {

        // DO NOT REPEAT IT
        // REMOVE THIS CODE AFTER WE ADD DataBase
        // !!!!!!!!!!!!!!
        private static List<GirlViewModel> Girls = new List<GirlViewModel>();

        public IActionResult Index()
        {
            if (!Girls.Any())
            {
                for (int i = 0; i < 5; i++)
                {
                    var viewModel = new GirlViewModel()
                    {
                        Rating = i * 10,
                        Name = $"Rita {i}",
                        Src = $"/images/girls/girl{i}.jpg",
                    };
                    Girls.Add(viewModel);
                }
            }

            return View(Girls);
        }

        // /Girl/Add  <== HTTP GET
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // /Girl/Add  <== HTTP POST
        [HttpPost]
        public IActionResult Add(GirlViewModel viewModel)
        {
            viewModel.CreationTime = DateTime.Now;

            // Remember new Girl
            Girls.Add(viewModel);

            return RedirectToAction("Index");
        }
    }
}
