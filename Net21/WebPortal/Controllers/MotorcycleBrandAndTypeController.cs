using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models;
using WebPortal.Models.Motorcycles;

namespace WebPortal.Controllers
{
    public class MotorcycleBrandAndTypeController : Controller
    {
        private IMotorcycleBrandRepositories _motorcycleBrandRepositories;
        private IMotorcycleTypeRepositories _motorcycleTypeRepositories;

        public MotorcycleBrandAndTypeController
            (
            IMotorcycleBrandRepositories motorcycleBrandRepositories,
            IMotorcycleTypeRepositories motorcycleTypeRepositories
            ) 
        {
            _motorcycleBrandRepositories = motorcycleBrandRepositories;
            _motorcycleTypeRepositories = motorcycleTypeRepositories;
        }

        public IActionResult Index()
        {
            var brands = _motorcycleBrandRepositories
                .GetAll()
                .Select(x => new BrandViewModel 
                {
                    BrandName = x.BrandName 
                })
                .ToList();

            var types = _motorcycleTypeRepositories
                .GetAll()
                .Select(x => new TypeViewModel 
                { 
                    TypeName = x.TypeName,
                    Description = x.Description
                })
                .ToList();

            var bransdAndTypesViewModel = new BrandsAndTypesViewModel 
            {
                Brands = brands,
                Types = types
            };
            return View(bransdAndTypesViewModel);
        }

        [HttpGet]
        public IActionResult AddBrandAndType() 
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult AddBrandAndType(AddBrandAndTypeViewModels addBrandAndTypeViewModels)
        {
            var brand = new Brand
            {
                BrandName = addBrandAndTypeViewModels.BrandName,
            };
            _motorcycleBrandRepositories.Add(brand);

            var type = new MotorcycleType
            {
                TypeName = addBrandAndTypeViewModels.TypeName,
                Description = addBrandAndTypeViewModels.Description
            };
            _motorcycleTypeRepositories.Add(type);

            return RedirectToAction("Index");
        }
    }
}
