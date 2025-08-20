using Microsoft.EntityFrameworkCore;
using UnderTheBridge.Data.Models;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Models.Marketplace.BaseItem;
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
        public DbSet<CoffeeProduct> CoffeeProducts { get; set; } 
        public DbSet<UserComment> UserComments { get; set; }

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
        public DbSet<BaseDevice> Devices { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<TypeDevice> TypeDevices { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Tourism> Tourisms { get; set; }

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

            //----------
            //UnderTheBridge
            modelBuilder
                .Entity<GuitarEntity>()
                .HasMany(guitar => guitar.Comments)
                .WithOne(comment => comment.Guitar)
                .OnDelete(DeleteBehavior.Cascade);
            //----------

            base.OnModelCreating(modelBuilder);
        }
    }
}
