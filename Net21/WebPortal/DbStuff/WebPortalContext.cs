using System.Reflection;
using Microsoft.EntityFrameworkCore;

using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CoffeShop;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;
using WebPortal.DbStuff.Models.HelpfullModels;
using WebPortal.DbStuff.Models.Marketplace;
using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Models.Notifications;
using WebPortal.DbStuff.Models.Tourism;


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

        //Tourism
        public DbSet<Tours> Tours { get; set; }
        public DbSet<TourPreview> TourPreviews { get; set; }

        //CoffeShop
        public DbSet<CoffeeProduct> CoffeeProducts { get; set; }
        public DbSet<UserComment> UserComments { get; set; }
        public DbSet<UserCoffeShop> UserCoffeShops { get; set; }


        /* CdekProject */
        public DbSet<CallRequest> CallRequests { get; set; }
        public DbSet<CdekChat> CdekChat { get; set; }

        public DbSet<Notification> Notifications { get; set; }

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

            modelBuilder
                .Entity<User>()
                .HasMany(u => u.CreatedCoffe)
                .WithOne(c => c.AuthorAdd)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<User>()
                .HasMany(user => user.CreatedTours)
                .WithOne(tour => tour.Author)
                .OnDelete(DeleteBehavior.NoAction);

            //====================
            // DON'T DELETE !!!
            modelBuilder
                .Entity<CommentEntity>()
                .HasOne(c => c.Guitar)
                .WithMany(g => g.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<CommentEntity>()
                .HasOne(c => c.Author)
                .WithMany(a => a.CommentsForGuitar)
                .OnDelete(DeleteBehavior.Cascade);
            // DON'T DELETE !!!
            //====================

            modelBuilder
                .Entity<User>()
                .HasMany(user => user.CallRequests)
                .WithOne(request => request.Author)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder
                .Entity<CdekChat>()
                .HasOne(x => x.Author)
                .WithMany(x => x.ChatMessagesCreated) 
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<CdekChat>()
                .HasMany(x => x.UserWhoViewedIt)
                .WithMany(x => x.ViewedChatMessages); 

            modelBuilder
                .Entity<Girl>()
                .HasMany(x => x.Animes)
                .WithMany(x => x.Characters);

            modelBuilder
                .Entity<Notification>()
                .HasOne(x => x.Author)
                .WithMany(x => x.NotificationCreatedByMe)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder
                .Entity<Notification>()
                .HasMany(x => x.UserWhoViewedIt)
                .WithMany(x => x.ViewedNotification);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}