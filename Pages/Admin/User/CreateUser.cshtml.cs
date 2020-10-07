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
    public class CreateUserModel : PageModel {
        private BloggieContext db;
        public CreateUserModel(BloggieContext db) => this.db = db;

        [BindProperty]
        public Bloggie.Models.User User { get; set; } = null;
        public void OnGet() { }

        //Lời nhắn trạng thái
        public string statusMsg { get; set; } = "init";
        public IActionResult OnPost() {
            //Kiểm tra email
            //Không phân biệt hoa thường 
            bool isExist = (db.Users.Where(u => u.Email.Equals(User.Email))).ToList().Any();
            
            //Input hợp lệ và email chưa tồn tại
            if (ModelState.IsValid && !isExist) {
                //Lưu email dưới dạng viết thường 
                User.Email=User.Email.ToLower();

                //Mã hoá mật khẩu
                User.Password = Hash.GetHashString(User.Password);
                
                //Thêm vào bảng Users
                db.Users.Add(User);
                
                //Lưu thay đổi 
                db.SaveChanges();
                
                //Chuyển hướng đến trang mặc định
                return Redirect("../../");
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