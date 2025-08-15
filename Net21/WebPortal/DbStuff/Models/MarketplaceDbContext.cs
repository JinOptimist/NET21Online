using Microsoft.EntityFrameworkCore;
using WebPortal.Models.marketplace;
using WebPortal.Models.Marketplace;

namespace WebPortal.Data
{
    public class MarketplaceDbContext : DbContext
    {
        public MarketplaceDbContext(DbContextOptions<MarketplaceDbContext> options)
            : base(options)
        {
        }

        public DbSet<MarketplaceRegistrationViewModel> Users { get; set; }
        public DbSet<ProductBase> Products { get; set; }
        public DbSet<LaptopViewModel> Laptops { get; set; }
        public DbSet<SmarphoneViewModel> Smartphones { get; set; }
        public DbSet<SmartWatchViewModel> SmartWatches { get; set; }
        public DbSet<HeadphonesViewModel> Headphones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка наследования для продуктов
            modelBuilder.Entity<ProductBase>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<LaptopViewModel>("Laptop")
                .HasValue<SmarphoneViewModel>("Smartphone")
                .HasValue<SmartWatchViewModel>("SmartWatch")
                .HasValue<HeadphonesViewModel>("Headphones");
        }
    }
}