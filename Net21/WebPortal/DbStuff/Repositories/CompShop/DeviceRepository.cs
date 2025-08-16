using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class DeviceRepository : BaseRepository<BaseDevice>
    {
        public DeviceRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<BaseDevice> GetAllPopular()
        {
            return _dbSet.Where(d => d.IsPopular == true).ToList();
        }

        /// <summary>
        /// Not Return List !!!!!
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BaseDevice> GetDeviceWithCategoryAndTypeDevice()
        {
            return _dbSet
                .Include(d => d.TypeDevice)
                .Include(d => d.Category);
        }
    }
}
