namespace Bloggie.Models
{
  public class User
  {
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public bool Active { get; set; }
    public UserRole Role { get; set; }
  }

  public enum UserRole
  {
    Writer,
    Moderator,
    Admin
  }
}