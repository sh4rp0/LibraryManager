using ErrorOr;

namespace LibraryManager.Domain.Common.Errors;

public static partial class Errors
{
    public static class Borrowing
    {
        public static Error BorrowingNotFound => Error.NotFound(
        code: "Borrowing.NotFound",
        description: "The borrowing was not found.");
    }
}
