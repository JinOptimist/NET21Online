using DevicesMinimalApi.DbStuff.Model;
using WebPortal.DbStuff.Repositories;

namespace DevicesMinimalApi.DbStuff.Repository
{
    public class DeviceRepository : BaseRepository<Device>
    {
        public DeviceRepository(DeviceDbContext portalContext) : base(portalContext)
        {
        }

        public List<string> GetAllNames()
        {
            return _dbSet.Select(x => x.Name).ToList();
        }
    }
}
