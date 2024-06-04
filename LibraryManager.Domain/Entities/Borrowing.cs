namespace LibraryManager.Domain.Entities
{
    public class Borrowing
    {
        public int Id { get; set; }

        public Guid UserId {  get; set; }

        public int BookId { get; set; }

        public bool IsReturned { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
