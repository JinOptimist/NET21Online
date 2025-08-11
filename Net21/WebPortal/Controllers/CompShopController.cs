using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using WebPortal.DbStuff;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.Models.CompShop;

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

            return View(startPageViewModel);
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
                : RedirectToAction("Index"); //В будущем заменить на каталог     
        }

        
        public IActionResult Delete(int Id)
        {
            var removeModel = _portalContext.Devices.First(x => x.Id == Id);
            var toHome = removeModel.IsPopular;
            _portalContext.Devices.Remove(removeModel);
            _portalContext.SaveChanges();

            return toHome
                ? RedirectToAction("Index") 
                : RedirectToAction("Index"); //В будущем заменить на каталог    
        }
    }
}
