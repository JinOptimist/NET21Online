using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff
{
    public class WebPortalContext : DbContext
    {
        public WebPortalContext(DbContextOptions<WebPortalContext> options) 
            : base(options) { }

        public DbSet<Girl> Girls { get; set; }
        public DbSet<Anime> Animes { get; set; }
        public DbSet<Tourism> Tourisms { get; set; }
    }
}
