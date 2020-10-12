using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Bloggie.Data;
namespace Bloggie.Pages
{
  public class LoginModel : PageModel
  {
    private readonly BloggieContext db;
    public LoginModel(BloggieContext db) => this.db = db;

    [BindProperty]
    public string Email { get; set; }
    
    [BindProperty, DataType(DataType.Password)]
    public string Password { get; set; }

    public string Message { get; set; }

    public async Task<IActionResult> OnPost()
    {
      if (Email == "welldey102@gmail.com")
      {
        var passwordHasher = new PasswordHasher<string>();
        if (Password == "12345")
        {
          var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, Email)
                    };
          var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
          await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
          return RedirectToPage("/admin/index");
        }
      }
      Message = "Email or Password incorrect!";
      return Page();
    }
  }
}
