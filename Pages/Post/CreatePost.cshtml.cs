using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinemo.Pages {
  public class CreatePostModel : PageModel {

    private CinemoContext db;
    public CreatePostModel(CinemoContext db) => this.db = db;
    //Tài khoản đăng đăng nhập
    public User Author { get; set; }
    [BindProperty]
    public string title { get; set; }
    [BindProperty]
    public string content { get; set; }
    [BindProperty]
    public string categoryId { get; set; }
    [BindProperty]
    public string tags { get; set; }
    public List<SelectListItem> categories { get; set; }

    //Tài khoản đang đăng nhập
    public User getUser() {
      //Lấy email hiện tại
      var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      //dùng email để truy vấn csdl
      return db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
    }
    public void OnGet() {
      //Tạo list category
      categories = db.Categories.Select(c =>
                              new SelectListItem {
                                Value = c.Id.ToString(),
                                Text = c.Name
                              }).ToList();
      //Tài khoản hiện tại
      Author = getUser();
    }

    public IActionResult OnPost() {
      //Tài khoản hiện tại
      Author = getUser();
      //Đối tượng post chứa các thông tin có được từ form
      Models.Post post = new Models.Post {
        Author = Author,
        AuthorId = Author.Id,
        Title = title,
        Content = content,
        Tags = tags,
        CategoryId = Int16.Parse(categoryId),
        Category = db.Categories.Find(Int32.Parse(categoryId)),
        CreatedAt = System.DateTime.Now
      };
      //Thêm vào csdl
      db.Posts.Add(post);
      //Lưu thay đổi
      db.SaveChanges();
      //Sau khi tạo post thành công sẽ chuyển hướng đến trang quản lý post
      return Redirect("./ListPost");
    }
  }
}
