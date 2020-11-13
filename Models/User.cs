using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Cinemo.Models {
  public class User {
    public int Id { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string FullName { get; set; }

    public bool Active { get; set; } = true;

    public UserRole Role { get; set; }

    public virtual List<Post> Posts {get; set;} = new List<Post>();
  }

  public enum UserRole {
    Writer, // Default role
    Moderator,
    Admin
  }
}