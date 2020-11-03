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

    //Đối tượng dùng để ánh xạ thông tin từ form input
    [BindProperty]
    [Required(ErrorMessage = "Category name is required.")]
    public string newName { get; set; }

    //Lời nhắn trạng thái
    //không bắt buộc có
    public string ErrorMessage { get; set; }
    public IActionResult OnPost() {
      if (!ModelState.IsValid) {
        return Page();
      }
      //Định dạng chuỗi
      newName = FormatString.Trim_MultiSpaces_Title(newName);
      //Kiểm tra tên thể loại đã tồn tại hay chưa
      bool isExist = (db.Categories.Where(c => c.Name.Equals(newName))).ToList().Any();

      //Input hợp lệ và tên thể loại chưa tồn tại
      if (ModelState.IsValid && !isExist) {
        Category category = new Category();
        category.Name = newName;
        //Thêm vào bảng Categories
        db.Categories.Add(category);

        //Lưu thay đổi 
        db.SaveChanges();

        //Chuyển hướng đến trang ListCategory
        return Redirect("./ListCategory");
      } else {
        //Tên thể loại đã tồn tại => tạo thất bại
        //Lời nhắn trạng thái
        ErrorMessage = newName + " existed";

        //Trả về trang hiện tại( trang tạo thể loại)
        return Page();
      }
    }
  }
}
