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
using Microsoft.AspNetCore.Http;

namespace Bloggie.Pages {
  public class LoginModel : PageModel {
    private readonly BloggieContext db;

    public LoginModel(BloggieContext db) => this.db = db;

    [BindProperty, DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter your email.")]
    public string Email { get; set; }

    [BindProperty, DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password.")]
    public string Password { get; set; }

    public string ErrorMessage { get; set; }

    public async Task<IActionResult> OnPost() {
      if (!ModelState.IsValid) {
        return Page();
      }

      var existed = db.Users.Where(user => user.Email == Email).FirstOrDefault();

      if (existed == null) {
        ErrorMessage = "Email not found.";
        return Page();
      }

      if (!Hash.GetHashString(Password).Equals(existed.Password)) {
        ErrorMessage = "Wrong password.";
        return Page();
      }

      var claims = new List<Claim> {
        new Claim(ClaimTypes.Email, Email),
        new Claim(ClaimTypes.Role, existed.Role.ToString()),
        new Claim(ClaimTypes.Name, existed.FullName)
      };

      var authenticationType = CookieAuthenticationDefaults.AuthenticationScheme;

      var claimsIdentity = new ClaimsIdentity(claims, authenticationType);

      await HttpContext.SignInAsync(authenticationType, new ClaimsPrincipal(claimsIdentity));

      HttpContext.Session.SetString("FullName", existed.FullName);
      HttpContext.Session.SetString("Role", existed.Role.ToString());
      HttpContext.Session.SetString("Email", existed.Email);

      return RedirectToPage("/Index");
    }
  }

}
