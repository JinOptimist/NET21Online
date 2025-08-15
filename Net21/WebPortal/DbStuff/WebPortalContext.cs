using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.Motorcycles;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;

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
        public DbSet<GuitarEntity> Guitars { get; set; }

        /* CompShop */
        public DbSet<BaseDevice> Devices { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<TypeDevice> TypeDevices { get; set; }
        public DbSet<News> News { get; set; }
        
        /* CdekProject */
        public DbSet<CdekCallRequest> CdekCallRequests { get; set; }
    }
}
