using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.Models;
using WebPortal.Models.Motorcycles;

namespace WebPortal.Controllers
{
    public class MotorcyclesController : Controller
    {
        private WebPortalContext _portalContext;
        public MotorcyclesController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        public IActionResult Index()
        {
            var motorcycles = _portalContext.Motorcycles
                .Take(10)
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
            var bikeRemove = _portalContext.Motorcycles.First(x => x.Id == Id);
            _portalContext.Motorcycles.Remove(bikeRemove);
            _portalContext.SaveChanges();

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
                BrandName = model.Name,
                ImageSrc = model.Src,
                Description = model.Description,
                Model = model.Name,
                MotorcycleType = model.Name
            };
            
            _portalContext.Motorcycles.Add(dbMotorcycle);
            _portalContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
