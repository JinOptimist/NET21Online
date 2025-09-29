using ProductServiceApi.DbStuff.Models;
using ProductServiceApi.Models;

namespace ProductsMinimalApi.Repositories
{
    public interface IProductRepository
    {
        Product? GetById(int id);
        List<Product> GetAll();
        List<Product> GetByCategory(string category);
        Product? Update(int id, UpdateProductDto updateDto);
        bool Delete(int id);
        Product Create(Product product);
    }
}