using WebPortal.DbStuff.Models.CompShop;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
