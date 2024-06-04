namespace LibraryManager.Application.Books.Common;

public record BookResult(
int Id,
string Title,
string Description,
string Author,
string ISBN,
int TotalCopies,
DateTime CreatedDateTime,
DateTime UpdatedDateTime);

