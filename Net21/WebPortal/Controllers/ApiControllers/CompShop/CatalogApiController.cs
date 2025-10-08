using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Repositories.Interfaces.CompShop;
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
        [AllowAnonymous]
        public IActionResult GetDevices(
            double? minPrice = null,
            double? maxPrice = null,
            int pageIndex = 1)
        {
            var devicesAll = _deviceRepository.GetIEnumerableDeviceWithCategoryAndType();

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

            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var devicesPage = devicesAll
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            var devices = devicesPage
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name,
                    price = x.Price,
                    imagePath = x.Image,
                    typeDeviceName = x.TypeDevice != null ? x.TypeDevice.Name : null,
                    categoryName = x.Category != null ? x.Category.Name : null,
                    canDelete = _compShopPermission.CanDelete()
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
