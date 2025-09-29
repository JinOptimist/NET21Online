using CoffeProductMinimalApi.DbStuff.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeProductMinimalApi.DbStuff
{
    public class CoffeProductDbContext:DbContext
    {
        public CoffeProductDbContext(DbContextOptions<CoffeProductDbContext> options)
             : base(options) { }

        public DbSet<CoffeProduct> CoffeProducts { get; set; }



    }
}
