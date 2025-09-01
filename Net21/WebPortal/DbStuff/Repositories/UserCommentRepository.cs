using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Repositories.Interfaces;
using WebPortal.Models.CoffeShop;

namespace WebPortal.DbStuff.Repositories
{
    public class UserCommentRepository : BaseRepository<UserComment>, IUserCommentRepository
    {
        public UserCommentRepository(WebPortalContext portalContext) : base(portalContext)
        {

        }

        public bool IsUniqNameCoffeUser(string? name)
        {
            return !_dbSet.Any(x => x.NameUser == name);
        }
    }
}
