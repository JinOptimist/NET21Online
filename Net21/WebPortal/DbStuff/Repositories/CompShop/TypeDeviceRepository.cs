using WebPortal.DbStuff.Models.CompShop;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class TypeDeviceRepository : BaseRepository<TypeDevice>
    {
        public TypeDeviceRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
