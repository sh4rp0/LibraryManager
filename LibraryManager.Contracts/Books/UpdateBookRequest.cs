namespace LibraryManager.Contracts.Books;

public record UpdateBookRequest(
    int Id,
    string Title,
    string Description,
    string Author,
    string ISBN,
    int TotalCopies);
