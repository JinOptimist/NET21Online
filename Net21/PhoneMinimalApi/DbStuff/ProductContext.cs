using Microsoft.EntityFrameworkCore;
using ProductServiceApi.DbStuff.Models;

namespace ProductServiceApi.DbStuff
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}