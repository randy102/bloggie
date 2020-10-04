using Bloggie.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Data
{
  public class BloggieContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite(@"Data source=Bloggie.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Seed();
    }
  }
}