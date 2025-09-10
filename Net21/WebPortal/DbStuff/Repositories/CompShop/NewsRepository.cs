using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Repositories.Interfaces.CompShop;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        public NewsRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<News> GetFirstNews(int count)
        {
            return _dbSet.OrderBy(d => d.DateCreate).Take(count).ToList();
        }
    }
}
