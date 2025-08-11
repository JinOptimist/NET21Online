using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Models.Motorcycles;

namespace WebPortal.Controllers
{
    public class MotorcyclesController : Controller
    {
        // DO NOT REPEAT IT
        // REMOVE THIS CODE AFTER WE ADD DataBase
        // It is necessary to redo it. Use only the database!!!!!!!!!!!!!!
        private static List<MotorcyclesViewModel> Motorcycles = new List<MotorcyclesViewModel>();

        public IActionResult Index()
        {
            if (!Motorcycles.Any()) 
            {
                for (int i = 0; i < 4; i++) 
                {
                    var model = new MotorcyclesViewModel();
                    model.Src = $"/images/motorcycles/moto{i}.jpg";
                    model.Name = $"BIKE{i + 1}";
                    model.Description = "Lorem ipsum, dolor sit amet consectetur adipisicing elit. Quisquam quo autem, perferendis voluptatum enim sunt non totam ducimus dignissimos animi cum velit ipsa veniam. Quasi porro fugiat commodi distinctio consequatur!";
                    Motorcycles.Add(model);
                }
            }

            return View(Motorcycles);
        }

        [HttpGet]
        public IActionResult AddBike() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBike(MotorcyclesViewModel model) 
        {
            Motorcycles.Add(model);
            return RedirectToAction("Index");
        }
    }
}
