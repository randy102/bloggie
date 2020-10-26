using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Pages
{
  public class LogoutModel : PageModel
  {
    public string ErrorMessage { get; set; }
    public string SuccessMessage {get; set; }
    public async Task<IActionResult> OnGet()
    {
        if(!HttpContext.User.Identity.IsAuthenticated)
            return RedirectToPage("/Index");
        try{
            await HttpContext.SignOutAsync();
            SuccessMessage = "Logout Success!";
        } catch(Exception error){
            ErrorMessage = error.Message;
        } 
        return Redirect("./Index");
    }
  }
}
