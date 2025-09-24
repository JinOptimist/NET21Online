using IdolMinimalApi.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace IdolMinimalApi.DbStuff
{
    public class IdolDbContext : DbContext
    {
        public IdolDbContext(DbContextOptions<IdolDbContext> options)
            : base(options) { }

        public DbSet<Idol> Idols { get; set; }
    }
}
