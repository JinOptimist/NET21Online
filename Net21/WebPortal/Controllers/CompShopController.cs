using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using WebPortal.Models.CompShop;
using WebPortal.Models.Moshko;

namespace WebPortal.Controllers
{
    public class CompShopController : Controller
    {
        public IActionResult Index()
        {
            var listDevices = new List<DeviceViewModel>();

            //Получение всех устройс, где Popular = true из бд

            if (!listDevices.Any())
            {
                // В будушем, сделать заполнение по умолчанию
                var listCategory = new List<Category>
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
                var listTypeDevice = new List<TypeDevice>
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

                listDevices.AddRange(new[]
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

            int rowSize = 3;

            var listDevicesOfThree = listDevices
            .Select((device, index) => new { device, index })
            .GroupBy(x => x.index / rowSize)
            .Select(g => g.Select(x => x.device).ToList())
            .ToList();

            var startPageViewModel = new StartPageViewModel();
            startPageViewModel.DevicesOfThree = listDevicesOfThree;

            return View(startPageViewModel);
        }
    }
}
