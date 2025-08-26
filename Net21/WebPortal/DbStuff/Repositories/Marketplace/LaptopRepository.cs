using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;

namespace WebPortal.DbStuff.Repositories.Marketplace
{
    public class LaptopRepository : BaseRepository<Laptop>, ILaptopRepository
    {
        public LaptopRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }
        public List<Laptop> GetByProcessor(string processor)
        {
            return _dbSet
                .Where(x => x.Processor.Contains(processor) && x.IsActive)
                .ToList();
        }

        public List<Laptop> GetWithRamGreaterThan(int ram)
        {
            return _dbSet
                .Where(x => x.RAM >= ram && x.IsActive)
                .OrderBy(x => x.RAM)
                .ToList();
        }

        public bool IsLaptopCategory(string? category)
        {
            return !_dbSet.Any(x => x.Name == category);
        }
    }
}