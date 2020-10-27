using System;
using System.Linq;
using Bloggie.Data;
using Bloggie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using bloggie.Utils;

namespace Bloggie.Pages {
  public class CreateCategoryModel : PageModel {

    private BloggieContext db;
    public CreateCategoryModel(BloggieContext db) => this.db = db;

    //Đối tượng dùng để ánh xạ thông tin từ form input
    [BindProperty]
    public Category category { get; set; }

    //Lời nhắn trạng thái
    //không bắt buộc có
    public string ErrorMessage { get; set; }
    public IActionResult OnPost() {
      //Định dạng chuỗi
      category.Name = FormatString.Trim_MultiSpaces_Title(category.Name);
      //Kiểm tra tên thể loại đã tồn tại hay chưa
      bool isExist = (db.Categories.Where(c => c.Name.Equals(category.Name))).ToList().Any();

      //Input hợp lệ và tên thể loại chưa tồn tại
      if (ModelState.IsValid && !isExist) {
        //Thêm vào bảng Categories
        db.Categories.Add(category);

        //Lưu thay đổi 
        db.SaveChanges();

        //Chuyển hướng đến trang ListCategory
        return Redirect("./ListCategory");
      } else {
        //Tên thể loại đã tồn tại => tạo thất bại
        //Lời nhắn trạng thái
        ErrorMessage = category.Name + " existed";

        //Trả về trang hiện tại( trang tạo thể loại)
        return Page();
      }
    }
  }
}
