namespace LibraryManager.Contracts.Books
{
    public record BookResponse(
        int Id,
        string Title,
        string Description,
        string Author,
        string ISBN,
        int TotalCopies,
        DateTime CreatedDateTime,
        DateTime UpdatedDateTime);
}
