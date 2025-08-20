using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces;

namespace WebPortal.DbStuff.Repositories.Interfaces.Marketplace
{
    public interface ILaptopRepository : IBaseRepository<Laptop>
    {
        List<Laptop> GetByProcessor(string processor);
        List<Laptop> GetWithRamGreaterThan(int ram);
    }
}