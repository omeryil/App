using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace App.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            AppDBContext db=new AppDBContext();
            if (db.User.ToList().Count < 1)
            {
                User u = new()
                {
                    username="demo",
                    password="demo"
                };
                db.User.Add(u);
                db.SaveChanges();
            }
            return View();
        }
        public IActionResult Login(string username, string password)
        {
            AppDBContext db = new AppDBContext();
            User u = db.User.Where(m => m.username.Equals(username) && m.password.Equals(password)).FirstOrDefault();

            if (u != null)
            {
                HttpContext.Session.SetString("isLogin", "true");
                return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/");
            }
        }

    }
}
