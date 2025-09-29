using Microsoft.EntityFrameworkCore;
using SpaceNewsMinApi.DbStuff.Models;

namespace SpaceNewsMinApi.DbStuff
{
    public class SpaceNewsDbContext : DbContext
    {
        public SpaceNewsDbContext(DbContextOptions<SpaceNewsDbContext> options)
            : base(options) { }
        public DbSet<SpaceNews> SpaceNews { get; set; }
    }
}
