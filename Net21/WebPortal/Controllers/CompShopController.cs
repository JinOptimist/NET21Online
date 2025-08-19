using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Repositories;
using WebPortal.DbStuff.Repositories.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.CompShop;
using WebPortal.Models.CompShop.Device;
using PathCompShop = WebPortal.Models.CompShop;

namespace WebPortal.Controllers
{
    public class CompShopController : Controller
    {
        private const int ROW_SIZE = 3;
        private const int COLOUM_SIZE = 6;

        private readonly DeviceRepository _deviceRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly TypeDeviceRepository _typeDeviceRepository;
        private readonly NewsRepository _newsRepository;
        private readonly ComputerRepository _computerRepository;



        public CompShopController(DeviceRepository devicerepository, CategoryRepository categoryRepository, TypeDeviceRepository typeDeviceRepository, NewsRepository newsRepository, ComputerRepository computerRepository)
        {
            _deviceRepository = devicerepository;
            _categoryRepository = categoryRepository;
            _typeDeviceRepository = typeDeviceRepository;
            _newsRepository = newsRepository;
            _computerRepository = computerRepository;
        }

        public IActionResult Index()
        {
            var devices = _deviceRepository.GetAllPopular();

            // Заменить на авто заполнение при создании проекта
            var categories = _categoryRepository.GetAll();
            var typeDevices = _typeDeviceRepository.GetAll();

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
                _categoryRepository.AddRange(categories);
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
                _typeDeviceRepository.AddRange(typeDevices);
            }

            var listDevicesOfThree = devices
            .Where(device => device.IsPopular)
            .Select((device, index) => new { device, index })
            .GroupBy(x => x.index / ROW_SIZE)
            .Select(g => g.Select(x => x.device).ToList())
            .ToList(); //Выбор популярных устройств по 3

            var listNews = _newsRepository.GetFirstNews(ROW_SIZE);

            var startPageViewModel = new StartPageViewModel();

            startPageViewModel.DevicesOfThree = listDevicesOfThree.Select(deviceList => deviceList.Select(device => new DeviceViewModel
            {
                 Name = device.Name,
                 Description = device.Description,
                 Price = device.Price,
                 Image = device.Image,
                 Id = device.Id,
                 Category = device.Category,
                 TypeDevice = device.TypeDevice,
                 IsPopular = device.IsPopular,
             }).ToList()).ToList();


            startPageViewModel.News = listNews.Select(nvm => new NewsViewModel
            {
                Id = nvm.Id,
                Name = nvm.Name,
                Text = nvm.Text,
                Description = nvm.Description,
                Image = nvm.Image,
                DateCreate = nvm.DateCreate,
            }).ToList();


            return View(startPageViewModel);
        }

        [HttpGet]
        public IActionResult Catalog(int pageIndex = 1)
        {
            var devices = _deviceRepository.GetIEnumerableDeviceWithCategoryAndType().ToList();
                
            // Скоро удалится
            var paginatedDevices = PathCompShop.CategoryViewModel.CreatePage(devices, pageIndex, COLOUM_SIZE * ROW_SIZE);

            return View(paginatedDevices);
        }

        [HttpPost]
        public IActionResult Catalog(int? minPrice, int? maxPrice, int pageIndex = 1)
        {

            var devicesAll = _deviceRepository.GetIEnumerableDeviceWithCategoryAndType().AsQueryable();

            if (minPrice != null)
            {
                devicesAll = devicesAll.Where(d => d.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                devicesAll = devicesAll.Where(d => d.Price <= maxPrice);
            }

            // Скоро удалится
            var paginatedDevices = PathCompShop.CategoryViewModel.CreatePage(devicesAll.ToList(), pageIndex, COLOUM_SIZE * ROW_SIZE);

            return View(paginatedDevices);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var addPageViewModel = new AddPageViewModel();

            var categories = _categoryRepository.GetAll();
            var typeDevices = _typeDeviceRepository.GetAll();

            addPageViewModel.Categoryes = _categoryRepository
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();

            addPageViewModel.TypeDevices = _typeDeviceRepository
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();

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

            var deviceDB = new Device
            {
                Name = device.Name,
                Description = device.Description,
                Price = device.Price,
                Image = device.Image,
                Id = device.Id,
                Category = _categoryRepository.GetFirstById(device.CategoryId),
                TypeDevice = _typeDeviceRepository.GetFirstById(device.TypeDeviceId),
                IsPopular = device.IsPopular,
            };

            switch (deviceDB.Category.Name)
            {
                case "Компьютер":
                    var comp = model.ComputerViewModel;
                    deviceDB.Computer = new Computer
                    {
                        Processor = comp!.Processor,
                        Ram = comp.Ram,
                        Memory = comp.Memory,
                        VideoCard = comp.VideoCard,
                        Motherboard = comp.Motherboard,
                        PowerUnit = comp.PowerUnit,
                        DeviceId = device.Id,
                    };
                    break;

                case "Ноутбук":
                    /*var nout = model.ComputerViewModel;
                    deviceDB.Laptop = new Laptop
                    {
                        Processor = comp.Processor,
                        Ram = comp.Ram,
                        Memory = comp.Memory,
                        VideoCard = comp.VideoCard,
                        Motherboard = comp.Motherboard,
                        PowerUnit = comp.PowerUnit,
                        DeviceId = device.Id,
                    }; И так далее */
                    break;

                default:
                    throw new Exception("Неизвестное значение.");
            }

            _deviceRepository.Add(deviceDB);

            return device.IsPopular
                ? RedirectToAction("Index")
                : RedirectToAction("Catalog");
        }


        public IActionResult Delete(int Id)
        {
            var removeModel = _deviceRepository.GetFirstById(Id);

            var toHome = removeModel.IsPopular;
            _deviceRepository.Remove(removeModel);

            return toHome
                ? RedirectToAction("Index")
                : RedirectToAction("Catalog");
        }

        public IActionResult ProductInfo(int id)
        {
            var deviceDB = _deviceRepository.GetFirstById(id);

            ProductInfoViewModel productInfoViewModel = new ProductInfoViewModel();
            
            var deviceViewModel = new DeviceViewModel
            {
                Name = deviceDB.Name,
                Description = deviceDB.Description,
                Price = deviceDB.Price,
                Image = deviceDB.Image,
                Category = deviceDB.Category,
                CategoryId = deviceDB.CategoryId,
                TypeDevice = deviceDB.TypeDevice,
                TypeDeviceId = deviceDB.TypeDeviceId,
                IsPopular = deviceDB.IsPopular,
            };

            productInfoViewModel.DeviceViewModel = deviceViewModel;

            deviceDB = _deviceRepository.GetDeviceWithAll(deviceDB);

            productInfoViewModel.ComputerViewModel = new ComputerViewModel
            {
                Processor = deviceDB.Computer.Processor,
                Ram = deviceDB.Computer.Ram,
                Memory = deviceDB.Computer.Memory,
                VideoCard = deviceDB.Computer.VideoCard,
                Motherboard = deviceDB.Computer.Motherboard,
                PowerUnit = deviceDB.Computer.PowerUnit,
            };

            return View(productInfoViewModel);
        }
    }
}
