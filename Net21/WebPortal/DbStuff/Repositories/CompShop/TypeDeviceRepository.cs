using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class TypeDeviceRepository : BaseRepository<TypeDevice>
    {
        public TypeDeviceRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
