using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories.Interfaces;


namespace WebPortal.DbStuff.Repositories
{
    public class ToursRepository : BaseRepository<Tours>, IToursRepository
    {
        public ToursRepository(WebPortalContext portalContext) : base(portalContext)
        {

        }
        public List<Tours> GetShopItemWithAuthor()
        {
            return _portalContext
                .TourismShops
                .Include(x => x.Author)
                .ToList();
        }
        public bool IsUniqName(string? name)
        {
            return !_dbSet.Any(x => x.TourName == name);
        }
    }
}
