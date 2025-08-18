using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class MotorcycleRepository : BaseRepository<Motorcycle>, IMotorcycleRepository
    {
        public MotorcycleRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
        public List<Motorcycle> GetNewMotorcycle()
        {
            return _portalContext
                .Motorcycles
                .OrderBy(x => x.Id)
                .Take(10)
                .ToList();
        }
    }
}
