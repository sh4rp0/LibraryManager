using LibraryManager.Application.Books.Commands;
using LibraryManager.Contracts.Books;
using LibraryManager.Domain.Entities;
using Mapster;

namespace LibraryManager.Api.Common.Mapping
{
    public class BookMappingCongig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateBookRequest, CreateCommand>()
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.Description, src => src.Description);

            config.NewConfig<Book, BookResponse>()
                .Map(dest => dest.ISBN, src => src.ISBN)
                .Map(dest => dest.Description, src => src.Description);
        }
    }
}
