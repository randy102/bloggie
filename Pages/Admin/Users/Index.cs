using System.Collections.Generic;
using System.Linq;
using Cinemo.Data;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Cinemo.Pages.Admin {
  public class ListUserModel : PageModel {
    private CinemoContext db;

    public ListUserModel(CinemoContext db) => this.db = db;

    public List<Cinemo.Models.User> Users { get; set; }

    public void OnGet() {
      Users = db.Users.Where(u => u.Role != UserRole.Admin).ToList();
    }

    public IActionResult OnPostChangeRole(int id) {
      var user = db.Users.Find(id);

      user.Role = (user.Role == UserRole.Moderator ? UserRole.Writer : UserRole.Moderator);

      db.SaveChanges();

      Users = db.Users.Where(u => u.Role != UserRole.Admin).ToList();

      return RedirectToPage();
    }

    public IActionResult OnPostChangeActive(int id) {
      var user = db.Users.Find(id);

      user.Active = (user.Active == true) ? false : true;

      db.SaveChanges();

      return RedirectToPage();
    }
  }
}
