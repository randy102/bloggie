using System;

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Bloggie.Utils;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Bloggie.Data;
namespace Bloggie.Pages {
  public class LoginModel : PageModel {
    private readonly BloggieContext db;
    public LoginModel(BloggieContext db) => this.db = db;

    [BindProperty]
    public string Email { get; set; }

    [BindProperty, DataType(DataType.Password)]
    public string Password { get; set; }

    public string Message { get; set; }

    public async Task<IActionResult> OnPost() {
      var existed = db.Users.Where(user => user.Email == Email).FirstOrDefault();

      if (existed != null) {
        if (Hash.GetHashString(Password).Equals(existed.Password)) {
          var claims = new List<Claim>
            {
              new Claim(ClaimTypes.Email, Email),
              new Claim(ClaimTypes.Role, existed.Role.ToString()),
              new Claim(ClaimTypes.Name, existed.FullName)
            };
          var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
          return RedirectToPage("/Index");
        }
      }
      Message = "Email or Password incorrect!";
      return Page();
    }
  }

}
