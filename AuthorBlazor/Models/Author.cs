using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AuthorBlazor.Models
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