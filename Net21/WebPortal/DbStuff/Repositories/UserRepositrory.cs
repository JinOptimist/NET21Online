using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories
{
    public class UserRepositrory : BaseRepository<User>, IUserRepositrory
    {
        public UserRepositrory(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
