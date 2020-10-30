using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Bloggie.Models;

namespace Bloggie.Pages {
  public class ChangeUserNameModel : PageModel {
    private BloggieContext db;

    public User CurrentUser { get; set; }

    public ChangeUserNameModel(BloggieContext db) => this.db = db;

    [BindProperty]
    public string FullName { get; set; }

    public IActionResult OnPost() {
      var CurrentEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      var CurrentUser = db.Users.Where(u => u.Email == CurrentEmail).First();
      CurrentUser.FullName = FullName;
      db.Users.Update(CurrentUser);
      db.SaveChanges();

      HttpContext.Session.SetString("FullName", FullName);

      return Redirect("/Profile");
    }
  }
}
