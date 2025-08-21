using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.Models;
using WebPortal.Models.Motorcycles;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.Controllers
{
    public class MotorcyclesController : Controller
    {
        private IMotorcycleRepository _motorcycleRepository;

        public MotorcyclesController(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public IActionResult Index()
        {
            var motorcycles = _motorcycleRepository
                .GetNewMotorcycle()
                .Select(dbMotorcycles => new MotorcyclesViewModel
                {
                    Name = dbMotorcycles.Model,
                    Src = dbMotorcycles.ImageSrc,
                    Description = dbMotorcycles.Description,
                    Id = dbMotorcycles.Id,
                })
                .ToList();
            return View(motorcycles);
        }
        public IActionResult Remove(int Id)
        {
            _motorcycleRepository.Remove(Id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddBike() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBike(MotorcyclesViewModel model) 
        {
            var dbMotorcycle = new Motorcycle()
            {
                ImageSrc = model.Src,
                Description = model.Description,
                Model = model.Name,
                MotorcycleType = model.Name
            };
            
            _motorcycleRepository.Add(dbMotorcycle);
            return RedirectToAction("Index");
        }
    }
}
