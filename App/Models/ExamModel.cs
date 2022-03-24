using System.Collections.Generic;

namespace App.Models
{
    public class ExamModel
    {
        public int StoryID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<QuestionForm> QuestionForms { get; set; }
    }
}
