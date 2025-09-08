using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class AnimeRepository : BaseRepository<Anime>, IAnimeRepository
    {
        public AnimeRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public Anime GetFirst()
        {
            return _dbSet.First();
        }
    }
}
