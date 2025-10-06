using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Enum;

namespace WebPortal.DbStuff.Repositories
{
    public class FestivalRepository : BaseRepository<Festival>, IFestivalRepository
    {
        public FestivalRepository(WebPortalContext portalContext)
            : base(portalContext) { }

        public List<Festival> GetAllWithGirls()
        {
            return _dbSet.Include(x => x.Girls).ToList();
        }

        public List<Festival> GetByTheme(FestivalTheme theme)
        {
            return _dbSet.Where(x => x.Theme == theme).OrderBy(x => x.Date).ToList();
        }

        public List<Festival> GetUpcoming(DateTime fromDate)
        {
            return _dbSet.Where(x => x.Date >= fromDate).OrderBy(x => x.Date).ToList();
        }
    }
}
