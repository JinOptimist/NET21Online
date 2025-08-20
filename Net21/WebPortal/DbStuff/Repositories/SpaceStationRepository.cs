using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class SpaceStationRepository : BaseRepository<SpaceNews>, ISpaceStationRepository
    {
        public SpaceStationRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
        public List<SpaceNews> FirstNews()
        {
            return _dbSet
                    .Include(x=>x.Author)
                    .ToList();
        }
    }

}
