using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("Story")]
    public class Story
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string StoryDate { get; set; }
        public bool Deleted { get; set; }
    }
}
