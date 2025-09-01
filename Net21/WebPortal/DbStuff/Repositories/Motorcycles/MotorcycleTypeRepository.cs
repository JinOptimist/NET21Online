using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Repositories.Interfaces.Motorcycles;

namespace WebPortal.DbStuff.Repositories.Motorcycles
{
    public class MotorcycleTypeRepositories : BaseRepository<MotorcycleType>, IMotorcycleTypeRepositories
    {
        public MotorcycleTypeRepositories(WebPortalContext portalContext) : base(portalContext)
        {
        }
        public bool IsUniqType(string? type)
        {
            return !_dbSet.Any(x => x.TypeName == type);
        }
    }
}
