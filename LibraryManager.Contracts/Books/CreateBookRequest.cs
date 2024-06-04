namespace LibraryManager.Contracts.Books;

public record CreateBookRequest(
    string Title,
    string Description,
    string Author,
    string ISBN,
    int TotalCopies);
