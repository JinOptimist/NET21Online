using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class GuitarRepository: BaseRepository<GuitarEntity>, IGuitarRepository
    {
        public GuitarRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
        public GuitarEntity GetById(int id)
        {
            return _dbSet.First(x => x.Id == id);
        }
    }
}
