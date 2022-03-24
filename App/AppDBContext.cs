using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App
{
    public class AppDBContext:DbContext
    {
        public DbSet<Story> Story { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=db.db");
        }

       
    }
}
