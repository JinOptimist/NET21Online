using ProductServiceApi.DbStuff.Models;
using ProductServiceApi.Models;

namespace ProductsMinimalApi.Services
{
    public interface IProductService
    {
        ProductDto? GetById(int id);
        List<ProductDto> GetAll();
        List<ProductDto> GetByCategory(string category);
        ProductDto? Update(int id, UpdateProductDto updateDto);
        bool Delete(int id);
       ProductDto Create (CreateProductDto createDto);
    }
}