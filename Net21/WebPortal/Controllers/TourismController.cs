using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Models.Tourism;

namespace WebPortal.Controllers
{
    public class TourismController : Controller
    {
        private static List<TourismViewModel> Tourism = new List<TourismViewModel>();
        public IActionResult Index()
        {
            if (!Tourism.Any())
            {
                var titleNames = new List<TourismViewModel>
            {
                new TourismViewModel
                {
                   Title = "Trip to Grodno",
                   URL = "/images/tourism/grodno.jpg",
                   Days = 3
                },
                new TourismViewModel
                {
                   Title = "Trip to Vitebs",
                   URL = "/images/tourism/vitebsk.jpg",
                   Days = 5
                }
            };
                Tourism.AddRange(titleNames);
            }

            return View(Tourism);
        }

        [HttpGet]
        public IActionResult AddContent()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddContent(TourismViewModel viewModel)
        {
            Tourism.Add(viewModel);
            return RedirectToAction("Index");
        }
    }
}
