using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff.Repositories.CompShop
{
    public class ComputerRepository : BaseRepository<Computer>, IComputerRepository
    {
        public ComputerRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
    }
}
