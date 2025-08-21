using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.CoffeShop;

namespace WebPortal.DbStuff.Repositories
{
    public class CoffeeProductRepository : BaseRepository<CoffeeProduct>, ICoffeeProductRepository
    {
        public CoffeeProductRepository(WebPortalContext portalContext) : base(portalContext)
        {

        }

        
        // In the future, create a separate page with information about coffee by pulling it out of the database.


    }
}
