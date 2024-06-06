using LibraryManager.Application.BookBorrowing.Commands;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Application.Common.Interfaces.Services;
using Moq;
using FluentAssertions;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Common.Errors;

namespace LibraryManager.Application.UnitTests.BookBorrowing.Commands;

public class BorrowCommandHandlerTests
{
    private readonly BorrowCommandHandler _handler;

    private readonly Mock<IBorrowingRepository> _mockBorrowingRepository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IBookRepository> _mockBookRepository;
    private readonly Mock<IDateTimeProvider> _mockDateTimeProvider;
    private readonly DateTime testTime = DateTime.Now.ToUniversalTime();


    public BorrowCommandHandlerTests()
    {
        _mockBorrowingRepository = new Mock<IBorrowingRepository>();
        _mockBorrowingRepository.Setup(m => m.AddAsync(It.IsAny<Borrowing>())).Returns((Borrowing b) => Task.FromResult(b));
        _mockUserRepository = new Mock<IUserRepository>();
        _mockUserRepository.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new User())!);
        _mockBookRepository = new Mock<IBookRepository>();
        _mockBookRepository.Setup(m => m.GetBookByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(new Book())!);
        _mockDateTimeProvider = new Mock<IDateTimeProvider>();
        _mockDateTimeProvider.Setup(m => m.UtcNow).Returns(testTime);
        _handler = new BorrowCommandHandler(_mockBookRepository.Object, _mockUserRepository.Object, _mockBorrowingRepository.Object, _mockDateTimeProvider.Object);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 62)]
    public async Task HandleBorrowCommand_WhenBorrowingIsValid_ShouldCreateAndReturnBorrowing(int bookId, int maxDaysUntilReturn)
    {
        // Arrange
        var userId = Guid.NewGuid();
        var borrowBookCommand = new BorrowCommand(userId, bookId, maxDaysUntilReturn);        

        // Act
        // Invoke the handler
        var result = await _handler.Handle(borrowBookCommand, default);

        // Assert
        // 1. Validate correct borrowing created based on command
        // 2. Borrowing added to repository
        result.IsError.Should().BeFalse();

        result.Value.UserId.Should().Be(userId);
        result.Value.BookId.Should().Be(bookId);
        result.Value.DueDate.Should().Be(testTime.Date.AddDays(maxDaysUntilReturn));
        result.Value.CreatedDateTime.Should().Be(testTime);

        _mockBorrowingRepository.Verify(m => m.AddAsync(result.Value), Times.Once());
    }

    [Fact]
    public async Task HandleBorrowCommand_WhenUserIsNotFound_ShouldReturnError()
    {
        // Arrange
        int bookId = 1;
        int maxDaysUntilReturn = 1;
        var userId = Guid.NewGuid();
        var borrowBookCommand = new BorrowCommand(userId, bookId, maxDaysUntilReturn);

        var mockUserRepositoryNull = new Mock<IUserRepository>();
        var handler = new BorrowCommandHandler(_mockBookRepository.Object, mockUserRepositoryNull.Object, _mockBorrowingRepository.Object, _mockDateTimeProvider.Object);

        // Act
        // Invoke the handler
        var result = await handler.Handle(borrowBookCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors[0].Should().Be(Errors.User.UserNotFound);

        _mockBorrowingRepository.Verify(m => m.AddAsync(result.Value), Times.Never());
    }

    [Fact]
    public async Task HandleBorrowCommand_WhenBookIsNotFound_ShouldReturnError()
    {
        // Arrange
        int bookId = 1;
        int maxDaysUntilReturn = 1;
        var userId = Guid.NewGuid();
        var borrowBookCommand = new BorrowCommand(userId, bookId, maxDaysUntilReturn);

        var mockBookRepositoryNull = new Mock<IBookRepository>();
        var handler = new BorrowCommandHandler(mockBookRepositoryNull.Object, _mockUserRepository.Object, _mockBorrowingRepository.Object, _mockDateTimeProvider.Object);

        // Act
        // Invoke the handler
        var result = await handler.Handle(borrowBookCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.Errors[0].Should().Be(Errors.Book.BookNotFound);

        _mockBorrowingRepository.Verify(m => m.AddAsync(result.Value), Times.Never());
    }
}
