using WebPortal.DbStuff.DataModels;

namespace WebPortal.DbStuff.Repositories.Interfaces.Marketplace
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<Product> GetActiveProducts();
        List<Product> GetByCategory(int categoryId);
        void Delete(Product product);
        Product? GetById(int id);
        List<MarketplaceCatalog> GetCatalog();
        Task<List<Product>> GetAllActiveAsync();
        Task<List<Product>> GetByTypeAsync(string productType);
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}