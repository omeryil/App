using App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App.Helpers;
using Microsoft.AspNetCore.Http;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!check())
                return Redirect("~/");
            AppDBContext db = new AppDBContext();
           
            List<Story> stories = MostRecent.getStoryList();
            stories = stories.Where(m => !db.Story.Select(m => m.Title).ToList().Contains(m.Title)).ToList();
            db.Story.AddRange(stories);
            db.SaveChanges();
            List<int> storyHasExam = db.Questions.Select(m => m.StoryID).ToList();
            stories = db.Story.Where(m=>!storyHasExam.Contains(m.ID) && !m.Deleted).Skip(0).Take(5).ToList();
            
            return View(stories);
        }
        [HttpPost]
        public IActionResult Saveform(List<QuestionForm> frm)
        {
            if (!check())
                return Redirect("~/");
            AppDBContext db = new AppDBContext();
            
            foreach (var item in frm)
            {
                Questions qs = new()
                {
                    StoryID=item.StoryID,
                    Question = item.Question,
                    Deleted =false
                };
                db.Questions.Add(qs);
                db.SaveChanges();
                int id = qs.ID;
                List<Answers> ans = new List<Answers>();
                foreach (var a in item.Answer)
                {
                    Answers answer = new() { 
                        Answer = a.Answer,
                        Correct= (bool)a.Correct,
                        QuestionID=id
                    };
                    db.Answers.Add(answer);
                    db.SaveChanges();
                }
                
            }
            List<int> storyHasExam = db.Questions.Select(m => m.StoryID).ToList();
            List<Story> stories = db.Story.Where(m => !storyHasExam.Contains(m.ID) && !m.Deleted).Skip(0).Take(5).ToList();
            return View("~/Views/Home/Index.cshtml",stories);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public bool check()
        {
            if (HttpContext.Session.GetString("isLogin") != null)
            {
                string l = HttpContext.Session.GetString("isLogin");
                if (l != null && l == "true")
                {
                    return true;
                }
            }
            return false;

        }
    }
}
