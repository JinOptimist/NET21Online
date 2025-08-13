using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class GirlRepository : BaseRepository<Girl>, IGirlRepository
    {
        public GirlRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<Girl> GetMostPopular()
        {
            return _dbSet
                .OrderBy(x => x.Size)
                .Take(10)
                .ToList();
        }
    }
}
