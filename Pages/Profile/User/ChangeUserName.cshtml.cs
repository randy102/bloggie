using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Data;
using System.Security.Claims;

namespace Bloggie.Pages
{
    public class ChangeUserNameModel : PageModel
    {
        private BloggieContext db;
        public ChangeUserNameModel(BloggieContext db) => this.db = db;

        [BindProperty]
        public string FullName {get; set;}

        public IActionResult OnPost(){
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var toUpdate = db.Users.Where(u => u.Email == userEmail).First();
            toUpdate.FullName = FullName;
            db.Users.Update(toUpdate);
            db.SaveChanges();
            return RedirectToPage("PersonalInformation");
        }
    }
}
