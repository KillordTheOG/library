using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class BookModel
    {
        public Guid Idbook { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        public string? Description { get; set; }
    }
}
