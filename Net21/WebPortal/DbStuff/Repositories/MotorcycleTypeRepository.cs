using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
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
