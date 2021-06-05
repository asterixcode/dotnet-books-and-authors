using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorAPI.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required, MaxLength(15)]
        public string FirstName { get; set; }
        
        [Required, MaxLength(15)]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}