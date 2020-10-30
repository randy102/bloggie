using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bloggie.Data;
using Bloggie.Models;
using System.Security.Claims;

namespace Bloggie.Pages {
  public class ListPostModel : PageModel {
    private BloggieContext db;
    public ListPostModel(BloggieContext db) => this.db = db;
    //Danh sách các bài post của tài khoản hiện tại
    public List<Bloggie.Models.Post> Posts { get; set; }
    //Tài khoản hiện tại
    public User Author { get; set; }
    //Id post sẽ được thay đổi trạng thái
    [BindProperty]
    public int postId { get; set; }

    //Tài khoản đang đăng nhập
    public User getUser() {
      //Lấy email hiện tại
      var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      //dùng email để truy vấn csdl
      return db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
    }
    public void OnGet() {
      Author = getUser();
      Posts = Author.Posts;
    }
    public void OnPost() {
      //Bài post cần phải thay đổi trạng thái
      var post = db.Posts.Find(postId);
      //Biến này dùng để lưu trữ trạng thái tiếp theo của Post( Post State)
      var state = -1;
      //  Draft thành Pending
      //   Published thành Unpublished 
      if (post.State == Bloggie.Models.PostState.Draft || post.State == Bloggie.Models.PostState.Published) {
        //Trạng thái tăng 1
        state = (int)post.State + 1;
      }
      //  Pending thành Draft
      //   Rejected thành Pending
      //   Unpublished thành Published  
      else {
        //Trạng thái giảm 1
        state = (int)post.State - 1;
      }
      //Thay đổi trạng thái của bài post
      post.State = (PostState)state;
      //Cập nhật thay đổi
      db.Update(post);
      //Lưu thay đổi
      db.SaveChanges();
      //Chuyển đang trang list Post
      OnGet();
    }
  }
}
