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

        public void Delete(Laptop laptop)
        {
            _portalContext.Laptops.Remove(laptop);
            _portalContext.SaveChanges();
        }

        public Laptop? GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<Laptop>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .ToListAsync();
        }
    }
}