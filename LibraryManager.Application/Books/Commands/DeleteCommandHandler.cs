using ErrorOr;
using LibraryManager.Application.Common.Interfaces.Persistence;
using LibraryManager.Domain.Common.Errors;
using MediatR;

namespace LibraryManager.Application.Books.Commands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, ErrorOr<bool>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            _bookRepository.DeleteBookAsync(request.Id);

            return true;
        }
    }
}
