using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Data;
using Bloggie.Models;
using System.Security.Claims;

namespace Bloggie.Pages
{
    public class PersonalInformationModel : PageModel
    {
        private BloggieContext db;
        public PersonalInformationModel(BloggieContext db) => this.db = db;

        public User UserProfile;

        public void OnGet()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            UserProfile = db.Users.Where(u => u.Email == userEmail).First();
        }
    }
}
