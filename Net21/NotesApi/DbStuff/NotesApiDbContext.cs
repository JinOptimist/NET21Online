using Microsoft.EntityFrameworkCore;
using NotesApi.DbStuff.Models;


namespace NotesApi.DbStuff;

public class NotesApiDbContext : DbContext
{
    public NotesApiDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Banner> Banners { get; set; }
}