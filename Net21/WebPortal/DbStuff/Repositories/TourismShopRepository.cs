using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notes;
using WebPortal.DbStuff.Models.Tourism;
using WebPortal.DbStuff.Repositories.Interfaces;


namespace WebPortal.DbStuff.Repositories
{
    public class TourismShopRepository : BaseRepository<TourismShop>, ITourismShopRepository
    {
        public TourismShopRepository(WebPortalContext portalContext) : base(portalContext)
        {

        }
        public List<TourismShop> GetShopItemWithAuthor()
        {
            return _portalContext
                .TourismShops
                .Include(x => x.AuthorName)
                .ToList();
        }
    }
}
