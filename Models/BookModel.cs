namespace Library.Models
{
    public class BookModel
    {
        public Guid Idbook { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? Description { get; set; }
    }
}
