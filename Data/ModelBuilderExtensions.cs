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
            Email = "admin@admin.com",
            FullName = "Admin",
            Password = Hash.GetHashString("123456"),
            Active = true,
            Role = UserRole.Admin
          },
          new User
          {
            Id = 2,
            Email = "mod@mod.com",
            FullName = "Moderator",
            Password = Hash.GetHashString("123456"),
            Active = true,
            Role = UserRole.Moderator
          },
          new User
          {
            Id = 3,
            Email = "writer@writer.com",
            FullName = "Writer",
            Password = Hash.GetHashString("123456"),
            Active = true,
            Role = UserRole.Writer
          }
      );

      modelBuilder.Entity<Category>().HasData(
        new Category{
          Id = 1,
          Name = "Công nghệ"
        },
        new Category{
          Id = 2,
          Name = "Giải trí"
        },
        new Category{
          Id = 3,
          Name = "Bóng đá"
        }
      );

      modelBuilder.Entity<Post>().HasData(
        new Post{
          Id = 1,
          AuthorId = 2,
          CategoryId = 1,
          Content = "The [ForeignKey] attribute overrides the default convention for a foreign key It allows us to specify the foreign key property in the dependent entity whose name does not match with the primary key property of the principal entity.",
          Title = "The [ForeignKey(name)] attribute",
          State = PostState.Draft,
          CreatedAt = System.DateTime.Now
        },
        new Post{
          Id = 2,
          AuthorId = 1,
          CategoryId = 3,
          Content = "In the above example, the [ForeignKey] attribute is applied on the StandardRefId and specified in the name of the navigation property Standard. ",
          Title = "Learn Entity Framework",
          State = PostState.Pending,
          CreatedAt = System.DateTime.Now 
        }
      );
      return modelBuilder;
    }
  }
}