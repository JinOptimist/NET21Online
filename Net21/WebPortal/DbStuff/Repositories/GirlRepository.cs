using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class GirlRepository : BaseRepository<Girl>, IGirlRepository
    {
        public GirlRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<Girl> GetAllWithAuthor()
        {
            return _dbSet
                .Include(x => x.Author)
                .ToList();
        }

        public List<Girl> GetMostPopular()
        {
            return _dbSet
                .Include(x => x.Author)
                .OrderBy(x => x.Size)
                .Take(50)
                .ToList();
        }

        public bool IsUniqName(string? name)
        {
            return !_dbSet.Any(x => x.Name == name);
        }
    }
}
