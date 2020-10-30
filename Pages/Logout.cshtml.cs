using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Pages {
  public class LogoutModel : PageModel {
    public async Task<IActionResult> OnGet() {
      if (!HttpContext.User.Identity.IsAuthenticated)
        return RedirectToPage("/Index");

      await HttpContext.SignOutAsync();

      HttpContext.Session.Clear();

      return Redirect("/Index");
    }
  }
}
