using ProductServiceApi.DTOs.Models;
using ProductsMinimalApi.DTOs.Models;

namespace ProductsMinimalApi.Services
{
    public interface IProductService
    {
        ProductDto? GetById(int id);
        List<ProductDto> GetAll();
        List<ProductDto> GetByCategory(string category);
        ProductDto? Update(int id, UpdateProductDto updateDto);
        bool Delete(int id);
        Task<ProductDto> AddAsync(CreateProductDto createDto);
    }
}