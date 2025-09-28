using Microsoft.EntityFrameworkCore;
using ProductServiceApi.Models;

namespace ProductServiceApi.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
    }
}