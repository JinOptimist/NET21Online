using Microsoft.EntityFrameworkCore;
using WebPortal.Models.Cartoon;

namespace WebPortal.DbStuff
{
    public class DbCartoonContext : DbContext
    {
        public DbCartoonContext(DbContextOptions<DbCartoonContext> options) : base(options)
        {
        }

        public DbSet<CartoonViewModel> Cartoons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartoonViewModel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}