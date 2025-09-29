using Microsoft.EntityFrameworkCore;
using ProductServiceApi.DbStuff;
using ProductServiceApi.DbStuff.Models;
using ProductServiceApi.Models;

namespace ProductsMinimalApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ProductContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Product Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                _logger.LogInformation("Product created with ID: {ProductId}", product.Id);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product: {ProductName}", product.Name);
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var productToRemove = _context.Products.FirstOrDefault(p => p.Id == id);
                if (productToRemove == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found for deletion", id);
                    return false;
                }

                _context.Products.Remove(productToRemove);
                _context.SaveChanges();
                _logger.LogInformation("Product with ID {ProductId} was deleted", id);
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error while deleting product {ProductId}", id);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while deleting product {ProductId}", id);
                return false;
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                var products = _context.Products.ToList();
                _logger.LogInformation("Retrieved {Count} products", products.Count);
                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products");
                return new List<Product>();
            }
        }

        public List<Product> GetByCategory(string category)
        {
            try
            {
                var products = _context.Products.Where(p => p.Category == category).ToList();

                if (!products.Any())
                {
                    _logger.LogInformation("No products found in category: {Category}", category);
                }
                else
                {
                    _logger.LogInformation("Found {Count} products in category {Category}", products.Count, category);
                }

                return products;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving products for category {Category}", category);
                return new List<Product>();
            }
        }

        public Product? GetById(int id)
        {
            try
            {
                var product = _context.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found", id);
                }
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product with ID {ProductId}", id);
                return null;
            }
        }

        public Product? Update(int id, UpdateProductDto updateDto)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                _logger.LogWarning("Product with ID {ProductId} not found for update", id);
                return null;
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(updateDto.Name))
                {
                    product.Name = updateDto.Name;
                }
                if (!string.IsNullOrWhiteSpace(updateDto.Description))
                {
                    product.Description = updateDto.Description;
                }
                if (updateDto.Price.HasValue)
                {
                    product.Price = updateDto.Price.Value;
                }
                if (!string.IsNullOrWhiteSpace(updateDto.Category))
                {
                    product.Category = updateDto.Category;
                }
                if (!string.IsNullOrWhiteSpace(updateDto.ImageUrl))
                {
                    product.ImageUrl = updateDto.ImageUrl;
                }

                _context.SaveChanges();
                _logger.LogInformation("Product with ID {ProductId} was updated", id);
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product {ProductId}", id);
                return null;
            }
        }
    }
}