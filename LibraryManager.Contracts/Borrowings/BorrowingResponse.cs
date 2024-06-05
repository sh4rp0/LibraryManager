namespace LibraryManager.Contracts.Borrowings;

public record BorrowingResponse(
    int Id,
    Guid UserId,
    int BookId,
    bool IsReturned,
    DateTime DueDate,
    DateTime CreatedDateTime);
