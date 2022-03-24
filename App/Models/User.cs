using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
