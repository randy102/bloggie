using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bloggie.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Bloggie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Pages
{
    public class CreatePostModel : PageModel
    {
        
    private BloggieContext db;
    public CreatePostModel(BloggieContext db) => this.db = db;
    [BindProperty]
    public User Author { get; set; }
    [BindProperty]
    public Models.Post post { get; set;}
   [BindProperty]
    public string categoryId { get; set;}
    public string msg { get; set; }
    public List<SelectListItem> categories { get; set; }

        public User getUser() {
            var email=HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            return db.Users.Where(u =>u.Email.Equals(email)).FirstOrDefault();
        }
        public void OnGet()
        {
            categories=db.Categories.Select(c => 
                                  new SelectListItem 
                                  {
                                      Value = c.Id.ToString(),
                                      Text =  c.Name
                                  }).ToList();
            Author=getUser();
        }

        public IActionResult OnPost() {
            Author=getUser(); 
            post.AuthorId=Author.Id;
            post.CategoryId=Int16.Parse(categoryId);
            post.Category=db.Categories.Find(post.CategoryId);
            post.CreatedAt=System.DateTime.Now;
            db.Posts.Add(post);
            db.SaveChanges();
            return Redirect("./ListPost");
         }
    }
}
