using ProductServiceApi.DbStuff.Models;
using ProductServiceApi.Models;
using ProductsMinimalApi.Repositories;

namespace ProductsMinimalApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public ProductDto? GetById(int id)
        {
            var product = _repository.GetById(id);
            return product != null ? MapToDto(product) : null;
        }

        public List<ProductDto> GetAll()
        {
            var products = _repository.GetAll();
            return products.Select(MapToDto).ToList();
        }

        public List<ProductDto> GetByCategory(string category)
        {
            var products = _repository.GetByCategory(category);
            return products.Select(MapToDto).ToList();
        }

        public ProductDto? Update(int id, UpdateProductDto updateDto)
        {
            var updated = _repository.Update(id, updateDto);
            return updated != null ? MapToDto(updated) : null;
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public ProductDto Create(CreateProductDto createDto)
        {
            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price,
                Category = createDto.Category,
                ImageUrl = createDto.ImageUrl,
                CreatedAt = DateTime.UtcNow
            };

            var created = _repository.Create(product);
            return MapToDto(created);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                ImageUrl = product.ImageUrl,
                CreatedAt = product.CreatedAt
            };
        }
    }
}