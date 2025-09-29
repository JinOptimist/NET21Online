using Microsoft.EntityFrameworkCore;
using ProductServiceApi.DbStuff;
using ProductServiceApi.DbStuff.Models;
using ProductServiceApi.Models;

namespace ProductsMinimalApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
        }

        public Product Create(Product product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding product: {ex.Message}");
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
                    return false;
                }

                _context.Products.Remove(productToRemove);
                _context.SaveChanges();
                Console.WriteLine("Product was deleted");
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database error while deleting product {id}: {dbEx.Message}");
                return false;
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                var products = _context.Products.ToList();
                Console.WriteLine($"{products.Count} products");
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while retrieving products: {ex.Message}");
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
                    Console.WriteLine($"No products found in category: {category}");
                }

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products for category {category}: {ex.Message}");
                return new List<Product>();
            }
        }

        public Product? GetById(int id)
        {
            try
            {
                return _context.Products.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product with ID {id}: {ex.Message}");
                return null;
            }
        }

        public Product? Update(int id, UpdateProductDto updateDto)
        {
            var product = _context.Products.Find(id);
            if (product == null) 
            {
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
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product {id}: {ex.Message}");
                return null;
            }
        }
    }
}
