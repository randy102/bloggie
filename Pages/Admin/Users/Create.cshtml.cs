using System.Linq;
using Cinemo.Utils;
using Cinemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Cinemo.Pages.Admin {
  public class CreateUserModel : PageModel {
    private CinemoContext db;

    public CreateUserModel(CinemoContext db) => this.db = db;

    [BindProperty]
    public new Cinemo.Models.User User { get; set; } = null;

    public string ErrorMessage { get; set; }

    public IActionResult OnPost() {
      if (!ModelState.IsValid)
        return Page();

      User.Email = User.Email.ToLower();

      bool isExist = (db.Users.Where(u => u.Email.Equals(User.Email))).ToList().Any();

      if (isExist) {
        ErrorMessage = User.Email + " existed";

        return Page();
      }

      User.Password = Hash.GetHashString(User.Password);

      db.Users.Add(User);

      db.SaveChanges();

      return Redirect("/Admin/Users");
    }
  }
}