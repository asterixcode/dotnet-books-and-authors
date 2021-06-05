using System;
using System.ComponentModel.DataAnnotations;

namespace AuthorBlazor.Models
{
    public class Book
    {
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a valid ISBN value")]
        public int Isbn { get; set; }
        
        [Required, MaxLength(40)]
        public string Title { get; set; }
        
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a valid year")]
        public int PublicationYear { get; set; }
        
        [Range(0, Int32.MaxValue, ErrorMessage = "Please enter a valid number of pages")]
        public int NumOfPages { get; set; }
        
        public string Genre { get; set; }
    }
}