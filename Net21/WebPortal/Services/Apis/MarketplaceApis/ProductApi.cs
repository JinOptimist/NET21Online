using WebPortal.DbStuff.Repositories.Interfaces.Marketplace;
using WebPortal.Models.marketplace.BaseViewModel;

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

        public async Task<List<ProductViewModel>> GetAllProducts()
        {
            var products = await Task.Run(
                () => _productRepository.GetAll().Where(p => p.IsActive)
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
                .ToList());
            return products;
        }
        public async Task<List<ProductViewModel>> GetProductsByTypeAsync(string productType)
        {
            var products = await Task.Run(() => _productRepository.GetAll()
                .Where(p => p.IsActive && p.ProductType == productType)
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
                .ToList());
            return products;
        }
        public async Task<ProductViewModel> GetProductByIdAsync(int id)
        {
            var product = await Task.Run(() => _productRepository.GetById(id));

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
                return await GetAllProducts();

            var products = await Task.Run(() => _productRepository.GetAll()
                .Where(p => p.IsActive &&
                    (p.Name.Contains(searchTerm) ||
                     p.Brand.Contains(searchTerm) ||
                     p.Description.Contains(searchTerm)))
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
                .ToList());

            return products;
        }

        public async Task<List<ProductViewModel>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var products = await Task.Run(() => _productRepository.GetAll()
                .Where(p => p.IsActive && p.Price >= minPrice && p.Price <= maxPrice)
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
                .ToList());

            return products;
        }

        public async Task<List<ProductViewModel>> GetLaptopsAsync()
        {
            var laptops = await Task.Run(() => _laptopRepository.GetAll()
                .Where(l => l.IsActive)
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
                .ToList());

            return laptops;
        }

    }
}
