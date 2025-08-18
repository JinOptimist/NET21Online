using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Models.Marketplace.BaseItem;
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
            var products = _productRepository.GetActiveProducts();
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
            var model = new LaptopViewModel
            {
                AllLaptops = _laptopRepository.GetAll()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult HighPerformanceLaptops()
        {
            var laptops = _laptopRepository
                .GetWithRamGreaterThan(32)
                .OrderByDescending(x => x.Processor);
            return View(laptops);
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
                    GraphicsCard = "Integrated"
                };

                _laptopRepository.Add(laptop);
                return RedirectToAction("Laptops");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            var model = new CatalogViewModel
            {
                Products = _productRepository.GetActiveProducts()
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