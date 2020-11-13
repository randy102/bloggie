using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Data;
using System.Security.Claims;

namespace Cinemo.Pages {
  public class ChangePasswordModel : PageModel {
    private CinemoContext db;
    public ChangePasswordModel(CinemoContext db) => this.db = db;

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string OldPassword { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost() {
      var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      var toUpdate = db.Users.Where(u => u.Email == userEmail).First();
      try {
        if (toUpdate.Password != Utils.Hash.GetHashString(OldPassword))
          throw new Exception("Password Incorrect!");

        toUpdate.Password = Utils.Hash.GetHashString(Password);
        db.Users.Update(toUpdate);
        db.SaveChanges();
        return RedirectToPage("Profile");
      } catch (Exception error) {
        ErrorMessage = error.Message;
        return Page();
      }
    }
  }
}
