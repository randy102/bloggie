using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Models;
using Bloggie.Data;

namespace Bloggie.Pages {
  public class ListCategoryModel : PageModel {
    private BloggieContext db;

    public ListCategoryModel(BloggieContext db) => this.db = db;

    public List<Category> Categories { get; set; }

    public void OnGet() {
      Categories = db.Categories.ToList();
    }

    public IActionResult OnPostDelete(int id) {
      var category = db.Categories.Find(id);

      if (category == null) {
        return NotFound();
      }

      db.Categories.Remove(category);

      db.SaveChanges();

      return Page();
    }
  }
}
