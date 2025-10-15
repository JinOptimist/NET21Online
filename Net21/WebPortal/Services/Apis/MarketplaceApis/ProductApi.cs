using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.Models.marketplace.BaseViewModel;
using WebPortal.Models.Marketplace;

namespace WebPortal.Services.Apis.MarketplaceApis
{
    public class ProductApi
    {
        private readonly IProductRepository _productRepository;
        private readonly ILaptopRepository _laptopRepository;

        public ProductApi(IProductRepository productRepository, ILaptopRepository laptopRepository)
        {
            _productRepository = productRepository;
            _laptopRepository = laptopRepository;
        }

        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            var entities = await _productRepository.GetAllActiveAsync();
            var products = entities
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ProductType = p.ProductType,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CreatedDate = p.CreatedDate,
                    IsActive = p.IsActive,
                    CategoryName = p.Category?.Name ?? "Без категории",
                    OwnerName = p.User?.UserName ?? "Неизвестно"
                })
                .ToList();

            return products;
        }

        public async Task<List<ProductViewModel>> GetProductsByTypeAsync(string productType)
        {
            var entities = await _productRepository.GetByTypeAsync(productType);
            var products = entities
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ProductType = p.ProductType,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CreatedDate = p.CreatedDate,
                    IsActive = p.IsActive,
                    CategoryName = p.Category?.Name ?? "Без категории",
                    OwnerName = p.User?.UserName ?? "Неизвестно"
                })
                .ToList();

            return products;
        }

        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null || !product.IsActive)
                return null;

            return new ProductViewModel
            {
                Id = product.Id,
                ProductType = product.ProductType,
                Name = product.Name,
                Brand = product.Brand,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CreatedDate = product.CreatedDate,
                IsActive = product.IsActive,
                CategoryName = product.Category?.Name ?? "Без категории",
                OwnerName = product.User?.UserName ?? "Неизвестно"
            };
        }

        public async Task<List<ProductViewModel>> SearchProductsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllProductsAsync();

            var entities = await _productRepository.GetAllActiveAsync();
            var products = entities
                .Where(p =>
                    (p.Name != null && p.Name.Contains(searchTerm)) ||
                    (p.Brand != null && p.Brand.Contains(searchTerm)) ||
                    (p.Description != null && p.Description.Contains(searchTerm)))
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ProductType = p.ProductType,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CreatedDate = p.CreatedDate,
                    IsActive = p.IsActive,
                    CategoryName = p.Category?.Name ?? "Без категории",
                    OwnerName = p.User?.UserName ?? "Неизвестно"
                })
                .ToList();

            return products;
        }

        public async Task<List<ProductViewModel>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var entities = await _productRepository.GetByPriceRangeAsync(minPrice, maxPrice);
            var products = entities
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    ProductType = p.ProductType,
                    Name = p.Name,
                    Brand = p.Brand,
                    Price = p.Price,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    CreatedDate = p.CreatedDate,
                    IsActive = p.IsActive,
                    CategoryName = p.Category?.Name ?? "Без категории",
                    OwnerName = p.User?.UserName ?? "Неизвестно"
                })
                .ToList();

            return products;
        }

        public async Task<List<ProductViewModel>> GetLaptopsAsync()
        {
            var entities = await _laptopRepository.GetAllActiveAsync();
            var laptops = entities
                .Select(l => new ProductViewModel
                {
                    Id = l.Id,
                    ProductType = l.ProductType,
                    Name = l.Name,
                    Brand = l.Brand,
                    Price = l.Price,
                    Description = l.Description,
                    ImageUrl = l.ImageUrl,
                    CreatedDate = l.CreatedDate,
                    IsActive = l.IsActive,
                    CategoryName = "Ноутбуки",
                    OwnerName = "Система"
                })
                .ToList();

            return laptops;
        }
    }
}
