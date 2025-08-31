using Microsoft.EntityFrameworkCore;
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
    }
}