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

    [BindProperty(Name = "error", SupportsGet = true)]
    public string ErrorMessage { get; set; }

 
    public User getUser() {
      var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      return db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
    }
    public void OnGet() {
      Author = getUser();
      Posts = Author.Posts;
    }
    public IActionResult OnPost() {
      var post = db.Posts.Find(postId);

      switch(post.State){
        case PostState.Draft:
          post.State = PostState.Pending;
          break;
        case PostState.Pending:
          post.State = PostState.Draft;
          break;
        case PostState.Unpublished:
          post.State = PostState.Published;
          break;
        case PostState.Published:
          post.State = PostState.Unpublished;
          break;
      }

      db.Update(post);
      db.SaveChanges();

      return RedirectToPage();
    }

    public IActionResult OnPostDelete(int id) {
      try{
        var post = db.Posts.Find(id);

        if(post.State.Equals(PostState.Pending))
          throw new Exception("Can not delete Pending Post!");

        db.Posts.Remove(post);
        db.SaveChanges();
        return RedirectToPage();
      } 
      catch(Exception error){
        ErrorMessage = error.Message;
        return RedirectToPage(new {error = error.Message});
      } 
      
    }
  }
}
