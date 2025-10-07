using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Repositories.Interfaces.CompShop;
using WebPortal.Models.CompShop.Device;
using WebPortal.Services.Permissions.Interface;

namespace WebPortal.Controllers.ApiControllers.CompShop
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogApiController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ICompShopPermission _compShopPermission;
        private const int PageSize = 12;

        public CatalogApiController(IDeviceRepository deviceRepository, ICompShopPermission compShopPermission)
        {
            _deviceRepository = deviceRepository;
            _compShopPermission = compShopPermission;
        }

        [HttpGet]
        public IActionResult GetDevices(
            CategoryEnum? category = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? brands = null,
            int? minRating = null,
            int pageIndex = 1)
        {
            var devicesAll = _deviceRepository.GetIEnumerableDeviceWithCategoryAndType().AsQueryable();

            if (category != null)
            {
                devicesAll = devicesAll.Where(d => d.CategoryEnum == category);
            }

            if (minPrice.HasValue)
            {
                devicesAll = devicesAll.Where(d => d.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                devicesAll = devicesAll.Where(d => d.Price <= maxPrice.Value);
            }

            var totalItems = devicesAll.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / PageSize);

            var devices = devicesAll
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize)
                .Select(x => new DeviceViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    ImagePath = x.Image,
                    TypeDevice = x.TypeDevice,
                    Category = x.Category,
                    CanDelete = _compShopPermission.CanDelete()
                })
                .ToList();

            return Ok(new
            {
                items = devices,
                pageIndex,
                totalPages,
                totalItems
            });
        }
    }
}
