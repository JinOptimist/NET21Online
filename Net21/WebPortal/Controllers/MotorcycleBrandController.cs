using Microsoft.AspNetCore.Mvc;
using WebPortal.Models.Motorcycles;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models;
using WebPortal.DbStuff.Models.Motorcycles;

namespace WebPortal.Controllers
{
    public class MotorcycleBrandController : Controller
    {
        private IMotorcycleBrandRepositories _motorcycleBrandRepositories;

        public MotorcycleBrandController(IMotorcycleBrandRepositories motorcycleBrandRepositories) 
        {
            _motorcycleBrandRepositories = motorcycleBrandRepositories;
        }

        public IActionResult Index()
        {
            var brandViewModel = _motorcycleBrandRepositories
                .GetAll()
                .Select(x => new BrandViewModel
                {
                    BrandName = x.BrandName, 
                })
                .ToList();
            return View(brandViewModel);
        }

        [HttpGet]
        public IActionResult AddBrand() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBrand(BrandViewModel brendViewModel)
        {
            var brand = new Brand
            {
                BrandName = brendViewModel.BrandName,
            };
            _motorcycleBrandRepositories.Add(brand);
            return RedirectToAction("Index");
        }
    }
}
