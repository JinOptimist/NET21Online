using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortal.Controllers.CustomAuthorizeAttributes;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Repositories.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces.CompShop;
using WebPortal.Enum;
using WebPortal.Models.CompShop;
using WebPortal.Models.CompShop.Device;
using WebPortal.Services;
using WebPortal.Services.Permissions.Interface;
using PathCompShop = WebPortal.Models.CompShop;

namespace WebPortal.Controllers
{
    [Authorize]
    public class CompShopController : Controller
    {
        private const int ROW_SIZE = 3;
        private const int COLOUM_SIZE = 6;

        private readonly IDeviceRepository _deviceRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly TypeDeviceRepository _typeDeviceRepository;
        private readonly INewsRepository _newsRepository;
        private readonly ICompShopPermission _compShopPermission;
        private readonly ICompShopFileService _compShopFileService;

        public CompShopController(IDeviceRepository devicerepository,
            CategoryRepository categoryRepository,
            TypeDeviceRepository typeDeviceRepository,
            INewsRepository newsRepository,
            ICompShopPermission compShopPermission,
            ICompShopFileService fileService)
        {
            _deviceRepository = devicerepository;
            _categoryRepository = categoryRepository;
            _typeDeviceRepository = typeDeviceRepository;
            _newsRepository = newsRepository;
            _compShopPermission = compShopPermission;
            _compShopFileService = fileService;
        }

        [AllowAnonymous]
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
                 ImagePath = device.Image,
                 Id = device.Id,
                 Category = device.Category,
                 TypeDevice = device.TypeDevice,
                 IsPopular = device.IsPopular,
                 CanDelete = _compShopPermission.CanDelete(),
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

        #region Catalog
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Catalog(int pageIndex = 1)
        {
            var devicesDb = _deviceRepository.GetIEnumerableDeviceWithCategoryAndType().ToList();

            var devicesViewModels = devicesDb.Select(x => new DeviceViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.Image,
                Id = x.Id,
                Category = x.Category,
                TypeDevice = x.TypeDevice,
                IsPopular = x.IsPopular,
                TypeDeviceId = x.TypeDeviceId,
                CategoryId = x.CategoryId,
                CanDelete = _compShopPermission.CanDelete(),
            }).ToList();

            // Скоро удалится
            var paginatedDevices = PathCompShop.CategoryViewModel.CreatePage(devicesViewModels, pageIndex, COLOUM_SIZE * ROW_SIZE);

            return View(paginatedDevices);
        }

        [HttpPost]
        [AllowAnonymous]
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

            var devicesViewModels = devicesAll.Select(x => new DeviceViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                ImagePath = x.Image,
                Id = x.Id,
                Category = x.Category,
                TypeDevice = x.TypeDevice,
                IsPopular = x.IsPopular,
                TypeDeviceId = x.TypeDeviceId,
                CategoryId = x.CategoryId,
                CanDelete = _compShopPermission.CanDelete(),
            }).ToList();

            // Скоро удалится
            var paginatedDevices = PathCompShop.CategoryViewModel.CreatePage(devicesViewModels, pageIndex, COLOUM_SIZE * ROW_SIZE);

            return View(paginatedDevices);
        }

        #endregion

        [HttpGet]
        [Role(Role.Admin)]
        public IActionResult Add()
        {
            var addPageViewModel = new AddPageViewModel();

            var categories = _categoryRepository.GetAll();
            var typeDevices = _typeDeviceRepository.GetAll();

            FillSelectListAdd(addPageViewModel);

            return View(addPageViewModel);
        }

        private void FillSelectListAdd(AddPageViewModel model)
        {
            model.Categoryes = _categoryRepository
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();

            model.TypeDevices = _typeDeviceRepository
                .GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        private string CreateImagePath(string oldPath)
        {
            var index = oldPath.IndexOf("wwwroot");

            if (index < 0)
            {
                throw new Exception("Wrong path to image");
            }

            var newPath = oldPath.Substring(index + 7); // 7 - wwwroot.Length

            if (newPath.StartsWith("\\") || newPath.StartsWith("/"))
            {
                newPath = newPath.Substring(1);
            }

            return "/" + newPath.Replace("\\", "/");
        }

        [HttpPost]
        [Role(Role.Admin)]
        public IActionResult Add(AddPageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                FillSelectListAdd(model);
                return View(model);
            }

            var deviceViewModel = model.DeviceViewModel;

            var deviceDB = new Device
            {
                Name = deviceViewModel.Name,
                Description = deviceViewModel.Description,
                Price = deviceViewModel.Price,
                Image = "defaul.Image",
                Category = _categoryRepository.GetFirstById(deviceViewModel.CategoryId),
                TypeDevice = _typeDeviceRepository.GetFirstById(deviceViewModel.TypeDeviceId),
                IsPopular = deviceViewModel.IsPopular,
            };

            deviceDB.CategoryEnum = GetCategoryEnum(deviceDB.Category.Name!);

            switch (deviceDB.CategoryEnum)
            {
                case CategoryEnum.Computer:
                    var comp = model.ComputerViewModel!;
                    deviceDB.Computer = new Computer
                    {
                        Processor = comp.Processor,
                        Ram = comp.Ram,
                        Memory = comp.Memory,
                        VideoCard = comp.VideoCard,
                        Motherboard = comp.Motherboard,
                        PowerUnit = comp.PowerUnit,
                    };
                    break;

                case CategoryEnum.Laptop:
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
                    throw new Exception("Данной катекогии не существует в CategoryEnum.");
            }

            _deviceRepository.Add(deviceDB);

            _compShopFileService.UploadAvatar(deviceViewModel.Image, deviceDB.Id);
            deviceDB.Image = CreateImagePath(_compShopFileService.GetPathToAvatar(deviceDB.Id));

            _deviceRepository.Update(deviceDB);

            return deviceDB.IsPopular
                ? RedirectToAction("Index")
                : RedirectToAction("Catalog");
        }

        [Role(Role.Admin)]
        public IActionResult Delete(int Id)
        {
            var removeModel = _deviceRepository.GetFirstById(Id);

            var toHome = removeModel.IsPopular;
            _deviceRepository.Remove(removeModel);

            return toHome
                ? RedirectToAction("Index")
                : RedirectToAction("Catalog");
        }

        [AllowAnonymous]
        public IActionResult ProductInfo(int id)
        {
            var deviceDB = _deviceRepository.GetFirstById(id);

            ProductInfoViewModel productInfoViewModel = new ProductInfoViewModel();
            
            var deviceViewModel = new DeviceViewModel
            {
                Name = deviceDB.Name,
                Description = deviceDB.Description,
                Price = deviceDB.Price,
                ImagePath = deviceDB.Image,
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

        private CategoryEnum GetCategoryEnum(string categoryName)
        {
            return categoryName switch
            {
                "Компьютер" => CategoryEnum.Computer,
                "Ноутбук" => CategoryEnum.Laptop,
                _ => throw new Exception($"Категория '{categoryName}' не зарегистрирована.")
            };
        }
    }
}
