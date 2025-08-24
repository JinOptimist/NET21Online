using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class MotorcycleBrandRepositories : BaseRepository<Brand>, IMotorcycleBrandRepositories
    {
        public MotorcycleBrandRepositories(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public bool IsUniqBrand(string? brand)
        {
            return !_dbSet.Any(x => x.BrandName == brand);
        }
    }
}
