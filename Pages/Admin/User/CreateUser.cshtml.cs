using System.Linq;
using Bloggie.Utils;
using Bloggie.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace Bloggie.Pages.Admin {
  public class CreateUserModel : PageModel {
    private BloggieContext db;
    public CreateUserModel(BloggieContext db) => this.db = db;

    //Đối tượng dùng để ánh xạ thông tin từ form input
    [BindProperty]
    public new Bloggie.Models.User User { get; set; } = null;

    //Lời nhắn trạng thái
    //không bắt buộc có
    public string statusMsg { get; set; } = string.Empty;
    public IActionResult OnPost() {
      //Kiểm tra email
      //Không phân biệt hoa thường 
      bool isExist = (db.Users.Where(u => u.Email.Equals(User.Email.ToLower()))).ToList().Any();

      //Input hợp lệ và email chưa tồn tại
      if (ModelState.IsValid && !isExist) {
        //Lưu email dưới dạng viết thường 
        User.Email = User.Email.ToLower();

        //Mã hoá mật khẩu
        User.Password = Hash.GetHashString(User.Password);

        //Thêm vào bảng Users
        db.Users.Add(User);

        //Lưu thay đổi 
        db.SaveChanges();

        //Chuyển hướng đến trang ListUser
        return Redirect("./ListUser");
      } else {
        //Email đã tồn tại => tạo tài khoản thất bại
        //Lời nhắn trạng thái
        statusMsg = "Not successful";

        //Trả về trang hiện tại( trang tạo tài khoản)
        return Page();
      }

    }
  }
}