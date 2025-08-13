using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.Models.CompShop;
using WebPortal.Models.NotesIndex;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebPortal.Controllers
{
    public class CompShopController : Controller
    {
        private const int ROW_SIZE = 3;

        private WebPortalContext _portalContext;

        public CompShopController(WebPortalContext portalContext)
        {
            _portalContext = portalContext;
        }

        public IActionResult Index()
        {
            var devices = _portalContext.Devices.Where(d => d.IsPopular == true).ToList();

            // Заменить на авто заполнение при создании проекта
            var categories = _portalContext.Categoryes.ToList();
            var typeDevices = _portalContext.TypeDevices.ToList();

            if (!categories.Any())
            {
                categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Компьютер"
                    },
                    new Category
                    {
                        Name = "Ноутбук"
                    },
                    new Category
                    {
                        Name = "Телефон"
                    },
                    new Category
                    {
                        Name = "Запчасти"
                    },
                };
                _portalContext.Categoryes.AddRange(categories);
                _portalContext.SaveChanges();
            }

            if (!typeDevices.Any())
            {
                typeDevices = new List<TypeDevice>
                {
                     new TypeDevice
                     {
                         Name = "Игровой",
                         Description = "Устройство предназначено для игр. Довольно мощный девайс."
                     },

                     new TypeDevice
                     {
                         Name = "Офисный",
                         Description = "Устройство для работы и офисных задач. Зачастую, имеет не самую сильную производительность."
                     },

                     new TypeDevice
                     {
                         Name = "Портативный",
                         Description = "Легкое и мобильное устройство, удобное для использования в дороге."
                     },

                     new TypeDevice
                     {
                         Name = "Бюджетный",
                         Description = "Устройства с браком, поломками или другими проблемами. Продаётся по занижиной цене."
                     }
                };
                _portalContext.TypeDevices.AddRange(typeDevices);
                _portalContext.SaveChanges();
            }

            var listDevicesOfThree = devices
            .Where(device => device.IsPopular)
            .Select((device, index) => new { device, index })
            .GroupBy(x => x.index / ROW_SIZE)
            .Select(g => g.Select(x => x.device).ToList())
            .ToList(); //Выбор популярных устройств по 3

            var listNews = _portalContext.News.OrderBy(d => d.DateCreate).Take(ROW_SIZE).ToList();

            var startPageViewModel = new StartPageViewModel();
            startPageViewModel.DevicesOfThree = listDevicesOfThree;
            startPageViewModel.News = listNews;


            /*for (int i = 0; i < 20; i++)
            {
                _portalContext.Devices.Add(
                    new BaseDevice
                    {
                        Name = "COMP" + i.ToString(),
                        Description = i.ToString() + "111111",
                        Price = i,
                        Image = "/images/CompShop/index/comp1.jpg",
                        Category = categories[0],
                        TypeDevice = typeDevices[0],
                        --//Processor = "AMD Ryzen 5 3600",
                        --//Ram = 16,
                        --//Memory = 240,
                        --//VideoCard = "NVIDIA GeForce RTX 3050",
                        --//Motherboard = "Gigabyte B650 AORUS ELITE AX DDR5 AM5",
                        --//PowerUnit = 1000

                    }
                );
            }

                _portalContext.SaveChanges();
            */

            return View(startPageViewModel);
        }

        [HttpGet]
        public IActionResult Catalog(int pageIndex = 1)
        {
            var coloumSize = 6;
            var devices = _portalContext.Devices
                .Include(d => d.TypeDevice)
                .Include(d => d.Category)
                .ToList();

            var paginatedDevices = Models.CompShop.CategoryViewModel.CreatePage(devices, pageIndex, coloumSize * ROW_SIZE);

            return View(paginatedDevices);
        }

        [HttpPost]
        public IActionResult Catalog(int? minPrice, int? maxPrice, int pageIndex = 1)
        {
            var coloumSize = 6;

            var devicesAll = _portalContext.Devices
                .Include(d => d.TypeDevice)
                .Include(d => d.Category)
                .AsQueryable();


            if (minPrice != null)
            {
                devicesAll = devicesAll.Where(d => d.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                devicesAll = devicesAll.Where(d => d.Price <= maxPrice);
            }

            var paginatedDevices = Models.CompShop.CategoryViewModel.CreatePage(devicesAll.ToList(), pageIndex, coloumSize * ROW_SIZE);

            return View(paginatedDevices);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var addPageViewModel = new AddPageViewModel();
            addPageViewModel.Categoryes = _portalContext.Categoryes.ToList();
            addPageViewModel.TypeDevices = _portalContext.TypeDevices.ToList();
            return View(addPageViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddPageViewModel model)
        {
            var device = model.DeviceViewModel;

            if (device == null)
            {
                return View(model);
            }

            device.Category = _portalContext.Categoryes.First(c => c.Id == device.CategoryId);
            device.TypeDevice = _portalContext.TypeDevices.First(t => t.Id == device.TypeDeviceId);

            _portalContext.Devices.Add(device);
            _portalContext.SaveChanges();

            return device.IsPopular
                ? RedirectToAction("Index")
                : RedirectToAction("Catalog");
        }


        public IActionResult Delete(int Id)
        {
            var removeModel = _portalContext.Devices.First(x => x.Id == Id);
            var toHome = removeModel.IsPopular;
            _portalContext.Devices.Remove(removeModel);
            _portalContext.SaveChanges();

            return toHome
                ? RedirectToAction("Index")
                : RedirectToAction("Catalog");
        }
    }
}
