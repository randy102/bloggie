using Cinemo.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinemo.Data
{
  public class CinemoContext : DbContext
  {
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite(@"Data source=Cinemo.db");
      optionsBuilder.UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Seed();
    }
  }
}