using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class UserCoffeShopRepositrory : BaseRepository<UserCoffeShop>, IUserCoffeShopRepository
    {
        public UserCoffeShopRepositrory(WebPortalContext portalContext) : base(portalContext)
        {
          
        }
    }
}
