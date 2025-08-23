using Microsoft.AspNetCore.Mvc;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;


namespace WebPortal.DbStuff.Repositories
{
    public class TourismShopRepository : BaseRepository<TourismShop>, ITourismShopRepository
    {
        public TourismShopRepository(WebPortalContext portalContext) : base(portalContext)
        {

        }
      
    }
}
