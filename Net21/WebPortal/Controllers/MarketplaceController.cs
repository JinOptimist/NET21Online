using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Models.Marketplace.BaseItem;
using WebPortal.Models.Marketplace;

namespace WebPortal.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly WebPortalContext _portalContext;
        private readonly ILogger<MarketplaceController> _logger;

        public MarketplaceController(WebPortalContext portalContext, ILogger<MarketplaceController> logger)
        {
            _portalContext = portalContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = _portalContext.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var categoryName = model.ProductType switch
                {
                    "Laptop" => "Ноутбуки",
                    "Smartphone" => "Смартфоны",
                    "SmartWatch" => "Умные часы",
                    "Headphones" => "Наушники",
                    _ => "Другое"
                };

                var category = _portalContext.Categories.FirstOrDefault(c => c.Name == categoryName);
                if (category == null)
                {
                    category = new Categories
                    {
                        Name = categoryName,
                        Description = $"Категория для {categoryName}"
                    };
                    _portalContext.Categories.Add(category);
                    _portalContext.SaveChanges();
                }
                Product product = model.ProductType switch
                {
                    "Laptop" => new Laptop
                    {
                        ProductType = model.ProductType,
                        Name = model.Name,
                        Brand = model.Brand,
                        Price = model.Price,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        CategoryId = category.Id,
                        Processor = model.Processor,
                        RAM = model.RAM ?? 8,
                        OS = model.OS ?? "Windows",
                        Storage = 512,
                        StorageType = "SSD",
                        GraphicsCard = "Integrated"
                    }
                };

                if (product == null)
                {
                    ModelState.AddModelError("", "Неизвестный тип продукта");
                    return View(model);
                }

                _portalContext.Products.Add(product);
                _portalContext.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении продукта");
                ModelState.AddModelError("", $"Ошибка при сохранении: {ex.Message}");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Catalog()
        {
            var model = new CatalogViewModel
            {
                Products = _portalContext.Products
                    .Where(p => p.IsActive)
                    .OrderByDescending(p => p.CreatedDate)
                    .ToList()
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