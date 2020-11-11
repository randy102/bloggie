using System;
using System.Linq;
using Bloggie.Data;
using Bloggie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bloggie.Utils;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Pages {
  public class CreateCategoryModel : PageModel {
    private BloggieContext db;

    public CreateCategoryModel(BloggieContext db) => this.db = db;

    [BindProperty]
    public string newName { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost() {
      if (!ModelState.IsValid)
        return Page();

      newName = FormatString.Trim_MultiSpaces_Title(newName);

      bool isExist = (db.Categories.Where(c => c.Name.Equals(newName))).ToList().Any();

      if (isExist) {
        ErrorMessage = newName + " existed";

        return Page();
      }

      Category category = new Category();

      category.Name = newName;

      db.Categories.Add(category);

      db.SaveChanges();

      return Redirect("/Admin/Categories");
    }
  }
}
