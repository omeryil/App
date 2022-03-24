using System;

namespace App.Models
{
    [Serializable]
    public class AnswerModel
    {
        public int ID { get; set; }
        public string Answer { get; set; }
        public Nullable<bool> isSelected { get; set; }
        public Nullable<bool> Correct { get; set; }
    }
}
