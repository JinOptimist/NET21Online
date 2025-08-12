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
        public DbSet<GuitarEntity> Guitars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuitarEntity>().HasData(
                new GuitarEntity { Id = 1, ImageUrl = "ibanez-grg121-card.webp", Mark = 4, Name = "Ibanez GRG121", Price = 400, ReviewAmount = 101, Status = GuitarEntity.AccessStatus.InShop },
                new GuitarEntity { Id = 2, ImageUrl = "les-paul.webp", Mark = 5, Name = "Gibson", Price = 10000, ReviewAmount = 1, Status = GuitarEntity.AccessStatus.InStock },
                new GuitarEntity { Id = 3, ImageUrl = "cort-x100-opbk-card.webp", Mark = 2, Name = "Cort X100 OBPK", Price = 200, ReviewAmount = 22, Status = GuitarEntity.AccessStatus.No },
                new GuitarEntity { Id = 4, ImageUrl = "ibanez-grg121-card.webp", Mark = 4, Name = "Ibanez GRG121", Price = 400, ReviewAmount = 101, Status = GuitarEntity.AccessStatus.InShop },
                new GuitarEntity { Id = 5, ImageUrl = "les-paul.webp", Mark = 5, Name = "Gibson", Price = 10000, ReviewAmount = 1, Status = GuitarEntity.AccessStatus.InStock },
                new GuitarEntity { Id = 6, ImageUrl = "cort-x100-opbk-card.webp", Mark = 2, Name = "Cort X100 OBPK", Price = 200, ReviewAmount = 22, Status = GuitarEntity.AccessStatus.No },
                new GuitarEntity { Id = 7, ImageUrl = "ibanez-grg121-card.webp", Mark = 4, Name = "Ibanez GRG121", Price = 400, ReviewAmount = 101, Status = GuitarEntity.AccessStatus.InShop },
                new GuitarEntity { Id = 8, ImageUrl = "les-paul.webp", Mark = 5, Name = "Gibson", Price = 10000, ReviewAmount = 1, Status = GuitarEntity.AccessStatus.InStock },
                new GuitarEntity { Id = 9, ImageUrl = "cort-x100-opbk-card.webp", Mark = 2, Name = "Cort X100 OBPK", Price = 200, ReviewAmount = 22, Status = GuitarEntity.AccessStatus.No },
                new GuitarEntity { Id = 10, ImageUrl = "ibanez-grg121-card.webp", Mark = 4, Name = "Ibanez GRG121", Price = 400, ReviewAmount = 101, Status = GuitarEntity.AccessStatus.InShop },
                new GuitarEntity { Id = 11, ImageUrl = "les-paul.webp", Mark = 5, Name = "Gibson", Price = 10000, ReviewAmount = 1, Status = GuitarEntity.AccessStatus.InStock },
                new GuitarEntity { Id = 12, ImageUrl = "cort-x100-opbk-card.webp", Mark = 2, Name = "Cort X100 OBPK", Price = 200, ReviewAmount = 22, Status = GuitarEntity.AccessStatus.No }
                );
        }
    }
}
