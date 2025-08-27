using System.Reflection;
using Microsoft.EntityFrameworkCore;

using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Models.HelpfullModels;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Models.Motorcycles;


namespace WebPortal.DbStuff
{
    public class WebPortalContext : DbContext
    {
        public WebPortalContext(DbContextOptions<WebPortalContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Girl> Girls { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<SpaceNews> SpaceNews { get; set; }
        public DbSet<Brand> MotorcyleBrands { get; set; }
        public DbSet<MotorcycleType> MotorcycleTypes { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }        

        // UnderTheBridge
        public DbSet<GuitarEntity> Guitars { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }

        //Marketplace
        public DbSet<Product> Products { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Smartphone> Smartphones { get; set; }
        public DbSet<SmartWatch> SmartWatches { get; set; }
        public DbSet<Headphones> Headphones { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }

        /* CompShop */
        public DbSet<Device> Devices { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<TypeDevice> TypeDevices { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Suggest> Suggests { get; set; } /*Helpfull*/
        public DbSet<Tourism> Tourisms { get; set; }
        
        //CoffeShop
        public DbSet<CoffeeProduct> CoffeeProducts { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserCoffeShop> UserCoffeShops { get; set; }

        
        /* CdekProject */
        public DbSet<CallRequest> CallRequests { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasMany(user => user.FavoriteGirls)
                .WithMany(girl => girl.UserWhoAddToFavorite);

            modelBuilder
                .Entity<User>()
                .HasMany(user => user.CreatedGirls)
                .WithOne(girl => girl.Author)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Motorcycle>()
                .HasOne(motorcycle => motorcycle.MotorcycleBrand)
                .WithMany(brand => brand.Motorcycles)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Motorcycle>()
                .HasOne(motorcycle => motorcycle.Type)
                .WithMany(type => type.Motorcycles)
                .OnDelete(DeleteBehavior.NoAction);
            //Marketplace Relationship

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.User)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProductImages>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Computer>()
                .HasOne(comp => comp.Device)
                .WithOne(device => device.Computer);


            //UnderTheBridge
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.CommentsForGuitar)
                .WithOne(c => c.Author);

            modelBuilder
                .Entity<GuitarEntity>()
                .HasMany(g => g.Comments)
                .WithOne(c => c.Guitar);
            //====================

            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}