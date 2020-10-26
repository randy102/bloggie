using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bloggie.Utils;
using Bloggie.Data;
using Bloggie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Pages {
  public class UpdateCategoryModel : PageModel {

    private BloggieContext db;
    public UpdateCategoryModel(BloggieContext db) => this.db = db;
    public string statusMsg { get; set; } = string.Empty;

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }


    [BindProperty]
    public Category category { get; set; } = null;
    public IActionResult OnGet() {
      category = (db.Categories.Where(c => c.Id == id)).FirstOrDefault();
      if (category == null) {
        return Redirect("./ListCategory");
      } else {
        return Page();
      }
    }
    public IActionResult OnPost() {
      //Định dạng chuỗi
      category.Name = FormatString.Trim_MultiSpaces_Title(category.Name);
      //Kiểm tra tên đã được sử dụng hay chưa
      bool isExist = (db.Categories.Where(c => c.Name.Equals(category.Name))).ToList().Any();
      //Input hợp lệ và tên thể loại chưa tồn tại
      if (ModelState.IsValid && !isExist) {
        //Cập nhật thay đổi
        db.Update(category);
        //Lưu thay đổi 
        db.SaveChanges();
        //Chuyển hướng đến trang ListCategory
        return Redirect("./ListCategory");
      } else {
        //Tên thể loại đã tồn tại => tạo thất bại
        //Lời nhắn trạng thái
        statusMsg = "Not successful";
        //Trả về trang hiện tại( trang cập nhật thể loại)
        return Page();
      }
    }
  }
}
