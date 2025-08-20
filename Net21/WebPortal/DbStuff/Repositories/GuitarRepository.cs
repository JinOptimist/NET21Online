using Microsoft.EntityFrameworkCore;
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

        public GuitarEntity? GetByIdWithComments(int id)
        {
            return _dbSet.Include(g => g.Comments).FirstOrDefault(g => g.Id == id);
        }

        public List<GuitarEntity> GetAllWithComments()
        {
            return _dbSet.Include(g => g.Comments).ToList();
        }
    }
}
