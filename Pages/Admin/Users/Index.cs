using System.Collections.Generic;
using System.Linq;
using Bloggie.Data;
using Bloggie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Bloggie.Pages.Admin {
  public class ListUserModel : PageModel {
    private BloggieContext db;

    public ListUserModel(BloggieContext db) => this.db = db;

    public List<Bloggie.Models.User> Users { get; set; }

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
