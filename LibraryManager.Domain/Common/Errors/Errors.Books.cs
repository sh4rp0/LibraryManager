using ErrorOr;

namespace LibraryManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Book
    {
        public static Error DuplicateBook => Error.Conflict(
            code: "Book.DuplicateBook",
            description: "Book is already in database");

        public static Error BookNotFound => Error.NotFound(
            code: "Book.NotFound",
            description: "The book was not found.");
    }
}
