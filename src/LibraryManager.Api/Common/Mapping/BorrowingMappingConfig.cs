using LibraryManager.Application.BookBorrowing.Commands;
using LibraryManager.Contracts.Borrowings;
using LibraryManager.Domain.Entities;
using Mapster;

namespace LibraryManager.Api.Common.Mapping;

public class BorrowingMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BorrowingRequest, BorrowCommand>();

        config.NewConfig<BorrowingResponse, Borrowing>();
    }
}
