using ProductServiceApi.Models;
using ProductsMinimalApi.DTOs.Models;

namespace ProductsMinimalApi.Repositories
{
    public interface IProductRepository
    {
        Product? GetById(int id);
        List<Product> GetAll();
        List<Product> GetByCategory(string category);
        Product? Update(int id, UpdateProductDto updateDto);
        bool Delete(int id);
        Task<Product> AddAsync(Product product);
    }
}