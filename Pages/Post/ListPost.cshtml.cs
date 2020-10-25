using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Data;

namespace Bloggie.Pages
{
  public class ListPostModel : PageModel
  {
    private BloggieContext db;
    public ListPostModel(BloggieContext db) => this.db = db;
    public List<Bloggie.Models.Post> Posts { get; set; }
    
    public void OnGet()
    {
      Posts = db.Posts.ToList();
    }
  }
}
