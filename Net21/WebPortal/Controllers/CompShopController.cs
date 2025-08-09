using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using WebPortal.Models.CompShop;
using WebPortal.Models.CompShop.Devices;

namespace WebPortal.Controllers
{
    public class CompShopController : Controller
    {
        private const int ROW_SIZE = 3;

        private static List<Category> listCategory = new List<Category>();
        private static List<TypeDevice> listTypeDevice = new List<TypeDevice>();

        private static List<DeviceViewModel> Devices = new List<DeviceViewModel>();

        public IActionResult Index()
        {
            var listDevices = new List<DeviceViewModel>();
            var listNews = new List<NewsViewModel>();

            //Получение всех устройс, где Popular = true из бд

            if (!Devices.Any())
            {
                // В будушем, сделать заполнение по умолчанию
                listCategory = new List<Category>
                { 
                    new Category
                    {
                        Id = 0,
                        Name = "Компьютер",
                    },
                    new Category
                    {
                        Id = 1,
                        Name = "Ноутбук",
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Телефон",
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Запчасти",
                    },
                };

                // В будушем, сделать заполнение по умолчанию
                listTypeDevice = new List<TypeDevice>
                {
                    new TypeDevice
                    {
                        Id = 0,
                        Name = "Игровой",
                    },
                    new TypeDevice
                    {
                        Id = 1,
                        Name = "Офисный",
                    },
                };

                Devices.AddRange(new[]
                {
                        new DeviceViewModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = listTypeDevice[0],
                            Category = listCategory[0],
                            Price = 3200,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                        new DeviceViewModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = listTypeDevice[1],
                            Category = listCategory[1],
                            Price = 3200,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                        new DeviceViewModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = listTypeDevice[0],
                            Category = listCategory[1],
                            Price = 3500,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                });
            }

            var listDevicesOfThree = Devices
            .Select((device, index) => new { device, index })
            .GroupBy(x => x.index / ROW_SIZE)
            .Select(g => g.Select(x => x.device).ToList())
            .ToList();

            //listNews = _db.CompShop.News.Take(ROW_SIZE).ToList();

            if (!listNews.Any())
            {
                listNews.AddRange(new[]
                {
                        new NewsViewModel
                        {
                            Name = "1. Режим использования масок и перчаток на территории магазинов",
                            Text = "Подробная информация о режимах использования масок и перчаток на территории магазинов \"ЛЕНТА\". Информация обновляется каждый будний день.",
                            Image = @"/images/Moshko/index/news1.jpg"
                        },
                        new NewsViewModel
                        {
                            Name = "2. Режим использования масок и перчаток на территории магазинов",
                            Text = "Подробная информация о режимах использования масок и перчаток на территории магазинов \"ЛЕНТА\". Информация обновляется каждый будний день.",
                            Image = @"/images/Moshko/index/news1.jpg"
                        },
                        new NewsViewModel
                        {
                            Name = "3. Режим использования масок и перчаток на территории магазинов",
                            Text = "Подробная информация о режимах использования масок и перчаток на территории магазинов \"ЛЕНТА\". Информация обновляется каждый будний день.",
                            Image = @"/images/Moshko/index/news1.jpg"
                        },
                });
            }

            var startPageViewModel = new StartPageViewModel();
            startPageViewModel.DevicesOfThree = listDevicesOfThree;
            startPageViewModel.News = listNews;

            return View(startPageViewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var AddModel = new AddPageViewModel();
            AddModel.Categoryes = listCategory;
            AddModel.TypeDevices = listTypeDevice;
            return View(AddModel);
        }

        [HttpPost]
        public IActionResult Add(DeviceViewModel device)
        {
            Devices.Add(device);
            return RedirectToAction("Index");
        }
    }
}
