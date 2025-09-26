using Microsoft.EntityFrameworkCore;
using WebPortal.DbStuff.Models.Notes;

namespace WebPortal.DbStuff;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options)
        : base(options) { }

    public DbSet<Note> Notes { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<NotificationNotes> Notifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .HasOne(n => n.Category)
            .WithMany(c => c.Notes)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Note>()
            .HasMany(n => n.Tags)
            .WithMany(t => t.Notes);
        
        modelBuilder
            .Entity<User>()
            .HasMany(user => user.FavoriteNotes)
            .WithMany(note => note.UserWhoAddToFavorite);

        modelBuilder
            .Entity<User>()
            .HasMany(user => user.CreatedNotes)
            .WithOne(note => note.Author)
            .OnDelete(DeleteBehavior.NoAction);
        
        modelBuilder
            .Entity<NotificationNotes>()
            .HasOne(x => x.Author)
            .WithMany(x => x.NotificationCreatedByMe)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder
            .Entity<NotificationNotes>()
            .HasMany(x => x.UserWhoViewedIt)
            .WithMany(x => x.ViewedNotification);

        base.OnModelCreating(modelBuilder);
    }
}