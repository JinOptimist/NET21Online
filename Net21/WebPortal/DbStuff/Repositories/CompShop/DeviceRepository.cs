using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.DataModels;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Repositories.Interfaces.CompShop;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
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
            var count = _dbSet
                .FromSqlRaw("SELECT * FROM Devices WHERE Name = {0}", name)
                .Count();

            return count == 0;

            // return !_dbSet.Any(x => x.Name == name);
        }
    }
}
