using Microsoft.EntityFrameworkCore;

using Bloggie.Utils;
using Bloggie.Models;

namespace Bloggie.Data
{
  public static class ModelBuilderExtensions
  {
    public static ModelBuilder Seed(this ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().HasData(
          new User
          {
            Id = 1,
            Email= "admin@admin.com",
            FullName = "Admin",
            Password = Hash.GetHashString("123456"),
            Active = true,
            Role = UserRole.Admin
          },
          new User
          {
            Id = 2,
            Email= "mod@mod.com",
            FullName = "Moderator",
            Password = Hash.GetHashString("123456"),
            Active = true,
            Role = UserRole.Moderator
          },
          new User
          {
            Id = 3,
            Email= "writer@writer.com",
            FullName = "Writer",
            Password = Hash.GetHashString("123456"),
            Active = true,
            Role = UserRole.Writer
          }
      );
      return modelBuilder;
    }
  }
}