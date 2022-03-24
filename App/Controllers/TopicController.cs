using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace App.Controllers
{
    public class TopicController : Controller
    {
        AppDBContext db = new AppDBContext();
        public IActionResult Index()
        {
            if(!check())
                return Redirect("~/");
            List<int> storyHasExam = db.Questions.Select(m => m.StoryID).ToList();
            var qs = db.Story.Where(m => storyHasExam.Contains(m.ID) && !m.Deleted).Select(m => m).ToList();
            return View(qs);
        }
        [HttpPost]
        public IActionResult Remove(int id)
        {
            if (!check())
                return Redirect("~/");
            Story s= db.Story.Where(m => m.ID==id).Select(m => m).FirstOrDefault();
            s.Deleted=true;
            db.SaveChanges();
            List<int> storyHasExam = db.Questions.Select(m => m.StoryID).ToList();
            var qs = db.Story.Where(m => storyHasExam.Contains(m.ID) && !m.Deleted).Select(m => m).ToList();
            return View("~/Views/Topic/Index.cshtml",qs);
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
