using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class UserCoffeShopRepositrory : BaseRepository<User>, IUserCoffeShopRepository
    {
        public UserCoffeShopRepositrory(WebPortalContext portalContext) : base(portalContext)
        {
          
        }
    }
}
