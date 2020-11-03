using System.Linq;
using Bloggie.Utils;
using Bloggie.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Bloggie.Pages.Admin {
  public class CreateUserModel : PageModel {
    private BloggieContext db;

    public CreateUserModel(BloggieContext db) => this.db = db;

    [BindProperty]
    public new Bloggie.Models.User User { get; set; } = null;

    public string ErrorMessage { get; set; }

    public IActionResult OnPost() {
      if (!ModelState.IsValid)
        return Page();

      bool isExist = (db.Users.Where(u => u.Email.Equals(User.Email.ToLower()))).ToList().Any();

      if (isExist) {
        ErrorMessage = User.Email + " existed";

        //Trả về trang hiện tại( trang tạo tài khoản)
        return Page();
      }

      User.Email = User.Email.ToLower();

      User.Password = Hash.GetHashString(User.Password);

      db.Users.Add(User);

      db.SaveChanges();

      return Redirect("/Admin/Users");
    }
  }
}