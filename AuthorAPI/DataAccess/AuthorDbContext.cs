using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI.DataAccess
{
    public class AuthorDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = C:\Users\lucas\Desktop\ExamA20-300709\AuthorAPI\Author.db");
        }
    }
}