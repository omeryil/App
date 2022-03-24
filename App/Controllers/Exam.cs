using App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace App.Controllers
{
    public class Exam : Controller
    {
        public IActionResult Index(int id)
        {
            if (!check())
                return Redirect("~/");
            AppDBContext db = new AppDBContext();
            List<Questions> qqq=db.Questions.ToList();  
            if (db.Questions.Where(m => m.StoryID == id).Any())
            {
                ExamModel ex = new ExamModel()
                {
                    StoryID = id,
                    Title = db.Story.Where(n => n.ID == id).Select(n => n.Title).FirstOrDefault(),
                    Content = db.Story.Where(n => n.ID == id).Select(n => n.Content).FirstOrDefault(),
                    QuestionForms = db.Questions.Where(m => !m.Deleted && m.StoryID == id).Select(m => new QuestionForm()
                    {

                        Question = m.Question,
                        Answer = db.Answers.Where(n => n.QuestionID == m.ID).OrderBy(m => m.ID).Select(n => new AnswerModel()
                        {
                            ID = n.ID,
                            Answer = n.Answer,
                            isSelected = false
                        }).ToArray()
                    }).ToList()
                };
                return View(ex);
            }
            else {
                return View("~/Views/Exam/Empty.cshtml");
            }

                
        }
        
        [HttpPost]
        public IActionResult Check(ExamResult result)
        {
            if (!check())
                return Redirect("~/");
            AppDBContext db = new AppDBContext();
            ExamModel ex = new ExamModel()
            {
                StoryID=result.id,
                Title = db.Story.Where(n => n.ID == result.id).Select(n => n.Title).FirstOrDefault(),
                Content = db.Story.Where(n => n.ID == result.id).Select(n => n.Content).FirstOrDefault(),
                QuestionForms = db.Questions.Where(m => !m.Deleted && m.StoryID == result.id).Select(m => new QuestionForm()
                {

                    Question = m.Question,
                    Answer = db.Answers.Where(n => n.QuestionID == m.ID).OrderBy(m => m.ID).Select(n => new AnswerModel()
                    {
                        ID = n.ID,
                        Answer = n.Answer,
                        Correct = n.Correct,
                        isSelected= result.ids.Contains(n.ID)
                    }).ToArray()
                }).ToList()
            };
            return View("~/Views/Exam/Index.cshtml",ex);
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
