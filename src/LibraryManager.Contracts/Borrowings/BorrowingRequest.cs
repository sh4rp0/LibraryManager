namespace LibraryManager.Contracts.Borrowings;

public record BorrowingRequest(
    Guid UserId,
    int BookId,
    int MaxDaysUntilReturn);
