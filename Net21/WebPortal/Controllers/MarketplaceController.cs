using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.Models.Marketplace;

namespace WebPortal.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly ILaptopRepository _laptopRepository;
        private readonly IProductRepository _productRepository;

        public MarketplaceController(
            ILaptopRepository laptopRepository,
            IProductRepository productRepository,
            ILogger<MarketplaceController> logger)
        {
            _laptopRepository = laptopRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var products = _productRepository.GetActiveProducts()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ProductType = p.ProductType,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CreatedDate = p.CreatedDate,
                    IsActive = p.IsActive
                })
                .ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new MarketplaceProductAddViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Laptops()
        {
            var laptops = _laptopRepository.GetAll()
                .Select(l => new LaptopViewModel
                {
                    Id = l.Id,
                    ProductType = l.ProductType,
                    Name = l.Name,
                    Brand = l.Brand,
                    Price = l.Price,
                    Description = l.Description,
                    ImageUrl = l.ImageUrl,
                    CreatedDate = l.CreatedDate,
                    IsActive = l.IsActive,
                    Processor = l.Processor,
                    RAM = l.RAM,
                    OS = l.OS,
                    Storage = l.Storage,
                })
                .ToList();

            var model = new LaptopViewModel
            {
                Laptops = laptops
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult HighPerformanceLaptops()
        {
            var laptops = _laptopRepository
                .GetWithRamGreaterThan(32)
                .OrderByDescending(x => x.Processor)
                .Select(l => new LaptopViewModel
                {
                    Id = l.Id,
                    ProductType = l.ProductType,
                    Name = l.Name,
                    Brand = l.Brand,
                    Price = l.Price,
                    Description = l.Description,
                    ImageUrl = l.ImageUrl,
                    CreatedDate = l.CreatedDate,
                    IsActive = l.IsActive,
                    Processor = l.Processor,
                    RAM = l.RAM,
                    OS = l.OS,
                    Storage = l.Storage,
                })
                .ToList();

            var model = new LaptopViewModel
            {
                Laptops = laptops
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(MarketplaceProductAddViewModel model)
        {
            if (model.ProductType == "Laptop")
            {
                var laptop = new Laptop
                {
                    ProductType = model.ProductType,
                    Name = model.Name,
                    Brand = model.Brand,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Processor = model.Processor,
                    RAM = model.RAM ?? 8,
                    OS = model.OS ?? "Windows",
                    Storage = 512,
                    StorageType = "SSD",
                    GraphicsCard = "Integrated",
                    ScreenSize = 15.6
                };

                _laptopRepository.Add(laptop);
                return RedirectToAction("Laptops");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            var products = _productRepository.GetActiveProducts()
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ProductType = p.ProductType,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CreatedDate = p.CreatedDate,
                    IsActive = p.IsActive
                })
                .ToList();

            var model = new CatalogViewModel
            {
                Products = products
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }
    }
}