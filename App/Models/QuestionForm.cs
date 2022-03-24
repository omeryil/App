using System;
using System.Collections.Generic;

namespace App.Models
{
    [Serializable]
    public class QuestionForm
    {
        public int StoryID { get; set; }
        public string Question { get; set; }
        public AnswerModel[] Answer { get; set; }
    }
}
