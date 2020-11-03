using System;
using System.Collections.Generic;
using System.Linq;
using Bloggie.Utils;
using Bloggie.Data;
using Bloggie.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Bloggie.Pages.Admin {
  public class ListUserModel : PageModel {
    private BloggieContext db;
    public ListUserModel(BloggieContext db) => this.db = db;
    public List<Bloggie.Models.User> Users { get; set; } = null;

    //Trả về danh sách các đối tượng cần quản lý ( không bao gồm admin)
    public List<Bloggie.Models.User> GetUsers() {
      return db.Users.Where(u => u.Role != UserRole.Admin).ToList();
    }

    //Xử lý yêu cầu hiện thị danh sách 
    public void OnGet() {
      Users = GetUsers();
    }

    //Xử lý yêu cầu thay đổi quyền của 1 tài khoản
    public IActionResult OnPostChangeRole(int userId) {

      //Tìm kiếm tài khoản dựa trên Id
      var user = db.Users.Find(userId);

      //Thay đổi quyền
      user.Role = (user.Role == UserRole.Moderator ? UserRole.Writer : UserRole.Moderator);

      //Lưu thay đổi
      db.SaveChanges();

      //Chuyển hướng đến trang hiện thị danh sách
      return Redirect("./ListUser");
    }

    //Xử lý yêu cầu thay đổi trạng thái của 1 tài khoản
    public IActionResult OnPostChangeActive(int userId) {

      //Tìm kiếm tài khoản dựa trên Id
      var user = db.Users.Find(userId);

      //Thay đổi trạng thái( block/ unblock)
      user.Active = (user.Active == true) ? false : true;

      //Lưu thay đổi
      db.SaveChanges();

      //Chuyển hướng đến trang hiện thị danh sách
      return Redirect("./ListUser");
    }
  }
}