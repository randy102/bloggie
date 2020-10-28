using System;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bloggie.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Bloggie.Data;
using Bloggie.Models;
namespace Bloggie.Pages {
  public class RegisterModel : PageModel {
    private readonly BloggieContext db;

    public RegisterModel(BloggieContext db) => this.db = db;

    [BindProperty, DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter your email.")]
    public string Email { get; set; }

    [BindProperty]
    [Required(ErrorMessage = "Please enter your name.")]
    public string FullName { get; set; }

    [BindProperty, DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password.")]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPost() {
      if (!ModelState.IsValid) {
        return Page();
      }

      try {
        var existed = await db.Users.Where(user => user.Email == Email).FirstOrDefaultAsync();
        if (existed != null) throw new Exception("Email existed.");

        var user = new User { Email = Email, FullName = FullName, Password = Hash.GetHashString(Password), Role = UserRole.Writer, Active = true };
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return Page();
      } catch (Exception error) {
        ErrorMessage = error.Message;
        return Page();
      }

    }
  }

}
