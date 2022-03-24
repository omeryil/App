using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("Answers")]
    public class Answers
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string Answer { get; set; }
        public bool Correct { get; set; }
        public int QuestionID { get; set; }
    }
}
