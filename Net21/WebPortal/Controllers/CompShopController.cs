using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebPortal.Models;

namespace WebPortal.Controllers
{
    public class CompShopController : Controller
    {
        public IActionResult Index()
        {
            var listDevices = new List<DeviceModel>();

            //Получение всех устройс, где Popular = true из бд

            if (!listDevices.Any())
            {
                listDevices.AddRange(new[]
                {
                        new DeviceModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = Typedevice.Computer,
                            DeviceCategory = DeviceCategory.Gaming,
                            Price = 3200,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                        new DeviceModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = Typedevice.Computer,
                            DeviceCategory = DeviceCategory.Gaming,
                            Price = 3200,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                        new DeviceModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = Typedevice.Computer,
                            DeviceCategory = DeviceCategory.Gaming,
                            Price = 3500,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                        new DeviceModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = Typedevice.Computer,
                            DeviceCategory = DeviceCategory.Gaming,
                            Price = 3200,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        },
                        new DeviceModel
                        {
                            Name = "RTX 4060 Ti/Ryzen 5 5600",
                            TypeDevice = Typedevice.Laptop,
                            DeviceCategory = DeviceCategory.Office,
                            Price = 3500,
                            Image = @"/images/Moshko/index/comp1.jpg"
                        }
                    });
            }

            return View(listDevices);
        }
    }
}
