using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("Questions")]
    public class Questions
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public int StoryID { get; set; }
        public string Question { get; set; }
        public bool Deleted { get; set; }
    }
}
