using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Repositories
{
    public class UserRepositrory : BaseRepository<User>, IUserRepositrory
    {
        public UserRepositrory(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
