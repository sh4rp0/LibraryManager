namespace LibraryManager.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string Author { get; set; } = null!;

        public string? ISBN { get; set; }

        public int TotalCopies { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public DateTime UpdatedDateTime { get; set; }
    }
}
