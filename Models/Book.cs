using DocumentFormat.OpenXml.Office2013.Excel;
using System.ComponentModel.DataAnnotations;

namespace Book_Keep.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string PublicationDate { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
        public Boolean Hidden { get; set; }
        public DateTime AddedOn { get; set; }
    }

    public class BookRequest
    {
        public int Id { get; set; }

        // Validations for title
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        // Validations for ISBN
        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "ISBN must be 10 or 13 digits.")]
        public string Isbn { get; set; }

        // Validations for Author
        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters.")]
        public string Author { get; set; }

        // Validations for Publisher
        [Required(ErrorMessage = "Publisher is required.")]
        [StringLength(100, ErrorMessage = "Publisher cannot exceed 100 characters.")]
        public string Publisher { get; set; }

        // Validations for Publication Date
        [Required(ErrorMessage = "Publication Date is required.")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}$", ErrorMessage = "PublicationDate must be in yyyy-MM-dd format.")]
        public string PublicationDate { get; set; }

        // Validations for Edition
        [Required(ErrorMessage = "Edition is required.")]
        [StringLength(20, ErrorMessage = "Edition cannot exceed 20 characters.")]
        public string Edition { get; set; }

        // Validations for Language
        [StringLength(50, ErrorMessage = "Language cannot exceed 50 characters.")]
        public string Language { get; set; }
    }

    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string PublicationDate { get; set; }
        public string Edition { get; set; }
        public string Language { get; set; }
        public Boolean Hidden { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
