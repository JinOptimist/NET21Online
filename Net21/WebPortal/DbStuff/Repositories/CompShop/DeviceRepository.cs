using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class DeviceRepository : BaseRepository<Device>
    {
        public DeviceRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<Device> GetAllPopular()
        {
            return _dbSet.Where(d => d.IsPopular == true).ToList();
        }

        public IEnumerable<Device> GetIEnumerableDeviceWithCategoryAndType()
        {
            return _dbSet
                .Include(d => d.TypeDevice)
                .Include(d => d.Category);
        }


        public Device GetDeviceWithAll(Device device)
        {
            return _dbSet
                .Include(d => d.TypeDevice)
                .Include(d => d.Computer)
                .Include(d => d.Category)
                .First(d => d.Id == device.Id);
        }

        public bool IsUniqName(string name)
        {
            return !_dbSet.Any(x => x.Name == name);
        }
    }
}
