using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.Models.marketplace.BaseViewModel;
using WebPortal.Models.Marketplace;
using WebPortal.Services;
using WebPortal.Enum;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using System.Text.Json;

namespace WebPortal.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly ILaptopRepository _laptopRepository;
        private readonly IProductRepository _productRepository;
        private readonly IAuthService _authService;
        private readonly IExportService _exportService;

        public MarketplaceController(
            ILaptopRepository laptopRepository,
            IProductRepository productRepository,
            IAuthService authService,
            IExportService exportService
            )
        {
            _laptopRepository = laptopRepository;
            _productRepository = productRepository;
            _authService = authService;
            _exportService = exportService;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            if (_authService.IsAuthenticated())
            {
                var id = _authService.GetId();
                var name = _authService.GetUser().UserName;

                viewModel.Id = id;
                viewModel.Name = name;
            }
            else
            {
                viewModel.Id = 0;
                viewModel.Name = "guest";
            }

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

            return View(viewModel);
        }

        [HttpGet]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult Add()
        {
            var model = new MarketplaceProductAddViewModel();
            return View(model);
        }

        [HttpPost]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult Add(MarketplaceProductAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
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
                    RAM = model.RAM.Value,
                    OS = model.OS,
                    Storage = 512,
                    StorageType = "SSD",
                    GraphicsCard = "Integrated"
                };

                _laptopRepository.Add(laptop);
                TempData["SuccessMessage"] = "Ноутбук успешно добавлен!";
                return RedirectToAction("Laptops");
            }
            TempData["ErrorMessage"] = "Выбранный тип товара пока не поддерживается";
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

        [HttpGet]
        public IActionResult Catalog()
        {
            var products = _productRepository.GetCatalog()
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
                    IsActive = p.IsActive,
                    CategoryName = p.CategoryName,
                    OwnerName = p.OwnerName
                })
                .ToList();

            var model = new CatalogViewModel
            {
                Products = products,
                CanAdd = _authService.GetRole() is Role.Admin || _authService.GetRole() is Role.MarketplaceModerator,
                CanEdit = _authService.GetRole() is Role.Admin || _authService.GetRole() is Role.MarketplaceModerator,
                CanDelete = _authService.GetRole() is Role.Admin || _authService.GetRole() is Role.MarketplaceModerator,
                CanExport = _authService.GetRole() is Role.Admin || _authService.GetRole() is Role.MarketplaceModerator
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }

        [HttpGet]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult Edit(int id, string productType)
        {
            if (productType == "Laptop")
            {
                var laptop = _laptopRepository.GetById(id);
                if (laptop == null)
                {
                    return NotFound();
                }

                var model = new MarketplaceProductAddViewModel
                {
                    Id = laptop.Id,
                    ProductType = laptop.ProductType,
                    Name = laptop.Name,
                    Brand = laptop.Brand,
                    Price = laptop.Price,
                    Description = laptop.Description,
                    ImageUrl = laptop.ImageUrl,
                    Processor = laptop.Processor,
                    RAM = laptop.RAM,
                    OS = laptop.OS
                };

                return PartialView("EditProductModal", model);
            }

            return NotFound();
        }

        [HttpPost]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult Edit(MarketplaceProductAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );
                return Json(new { success = false, errors });
            }

            if (model.ProductType == "Laptop")
            {
                var laptop = _laptopRepository.GetById(model.Id.Value);
                if (laptop == null)
                {
                    return Json(new { success = false, message = "Товар не найден" });
                }

                laptop.Name = model.Name;
                laptop.Brand = model.Brand;
                laptop.Price = model.Price;
                laptop.Description = model.Description;
                laptop.ImageUrl = model.ImageUrl;
                laptop.Processor = model.Processor;
                laptop.RAM = model.RAM.Value;
                laptop.OS = model.OS;

                _laptopRepository.Update(laptop);

                return Json(new { success = true, message = "Товар успешно обновлен!" });
            }

            return Json(new { success = false, message = "Тип товара не поддерживается" });
        }

        [HttpPost]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult DeleteAjax(int id, string productType)
        {
            try
            {
                if (productType == "Laptop")
                {
                    var laptop = _laptopRepository.GetById(id);
                    if (laptop != null)
                    {
                        _laptopRepository.Delete(laptop);
                        return Json(new { success = true, message = "Ноутбук успешно удален!" });
                    }
                }
                else
                {
                    var product = _productRepository.GetById(id);
                    if (product != null)
                    {
                        _productRepository.Delete(product);
                        return Json(new { success = true, message = "Товар успешно удален!" });
                    }
                }

                return Json(new { success = false, message = "Товар не найден" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ошибка при удалении: {ex.Message}" });
            }
        }

        [HttpPost]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult Delete(int id, string productType)
        {
            try
            {
                if (productType == "Laptop")
                {
                    var laptop = _laptopRepository.GetById(id);
                    if (laptop != null)
                    {
                        _laptopRepository.Delete(laptop);
                        return Json(new { success = true, message = "Ноутбук успешно удален!" });
                    }
                }
                else
                {
                    var product = _productRepository.GetById(id);
                    if (product != null)
                    {
                        _productRepository.Delete(product);
                        return Json(new { success = true, message = "Товар успешно удален!" });
                    }
                }

                return Json(new { success = false, message = "Товар не найден" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Ошибка при удалении: {ex.Message}" });
            }
        }

        [HttpGet]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult Export()
        {
            string content = _exportService.ExportProducts();
            string fileName = "catalog_export_.txt";

            var bytes = System.Text.Encoding.UTF8.GetBytes(content);

            return File(bytes, "text/plain", fileName);
        }

        [HttpPost]
        [Role(Role.Admin, Role.MarketplaceModerator)]
        public IActionResult ExportToFolder()
        {
            string filePath = _exportService.ExportProductsToFile();
            return RedirectToAction("Catalog");
        }

        public IActionResult SupportChat()
        {
            return View();
        }
    }
}