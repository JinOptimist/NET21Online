using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.DataModels;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;

namespace WebPortal.DbStuff.Repositories.Marketplace
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(WebPortalContext portalContext) : base(portalContext)
        {
        }

        public List<Product> GetActiveProducts()
        {
            return _dbSet.Where(x => x.IsActive).ToList();
        }

        public List<Product> GetByCategory(int categoryId)
        {
            return _dbSet.Where(x => x.CategoryId == categoryId).ToList();
        }

        public void Delete(Product product)
        {
            _portalContext.Products.Remove(product);
            _portalContext.SaveChanges();
        }

        public Product? GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public List<MarketplaceCatalog> GetCatalog()
        {
            FormattableString fs = @$"
SELECT 
    p.Id,
    p.ProductType,
    p.Name,
    p.Brand,
    p.Price,
    p.Description,
    p.ImageUrl,
    p.CreatedDate,
    p.IsActive,
    COALESCE(c.Name, 'Без категории') AS CategoryName,
    COALESCE(u.UserName, 'Не указан') AS OwnerName
FROM Products p
    LEFT JOIN Categories c ON p.CategoryId = c.Id
    LEFT JOIN Users u ON p.OwnerId = u.Id
WHERE p.IsActive = 1
ORDER BY p.CreatedDate DESC";

            var response = _portalContext.Database
                .SqlQuery<MarketplaceCatalog>(fs)
                .ToList();

            return response;
        }

        public async Task<List<Product>> GetAllActiveAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .ToListAsync();
        }

        public async Task<List<Product>> GetByTypeAsync(string productType)
        {
            return await _dbSet
                .Where(x => x.IsActive && x.ProductType == productType)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _dbSet
                .Where(x => x.IsActive && x.Price >= minPrice && x.Price <= maxPrice)
                .ToListAsync();
        }
    }
}