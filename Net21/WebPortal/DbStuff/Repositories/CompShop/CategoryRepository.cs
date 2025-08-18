using WebPortal.DbStuff.Models.CompShop;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
