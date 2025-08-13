using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebPortal.DbStuff.Models;
using WebPortal.DbStuff.Models.CompShop;
using WebPortal.DbStuff.Models.CompShop.Devices;

namespace WebPortal.DbStuff
{
    public class WebPortalContext : DbContext
    {
        public WebPortalContext(DbContextOptions<WebPortalContext> options) 
            : base(options) { }

        public DbSet<Girl> Girls { get; set; }
        public DbSet<Anime> Animes { get; set; }
        
        public DbSet<CoffeeProduct> CoffeeProducts { get; set; } 

        public DbSet<UserComment> UserComments { get; set; }




        /* CompShop */
        public DbSet<BaseDevice> Devices { get; set; }
        public DbSet<Category> Categoryes { get; set; }
        public DbSet<TypeDevice> TypeDevices { get; set; }
        public DbSet<News> News { get; set; }
    }
}
