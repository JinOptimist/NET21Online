namespace WebPortal.DbStuff.Repositories.Interfaces.Marketplace
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<Product> GetActiveProducts();
        List<Product> GetByCategory(int categoryId);
        void Delete(Product product);
        Product? GetById(int id);
    }
}