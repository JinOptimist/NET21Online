using WebPortal.DbStuff.Models.Marketplace.BaseItem;

namespace WebPortal.DbStuff.Repositories.Interfaces.Marketplace
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<Product> GetActiveProducts();
        List<Product> GetByCategory(int categoryId);
    }
}