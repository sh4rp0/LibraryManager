using LibraryManager.Application.Books.Commands;
using LibraryManager.Contracts.Books;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ErrorOr;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Common.Errors;
using LibraryManager.Application.Books.Queries;

namespace LibraryManager.Api.Controllers
{
    [Route("books")]
    public class BooksController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public BooksController(IMediator mediator, IMapper mapper, ProblemDetailsFactory problemDetailsFactory) : base(problemDetailsFactory)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet()]
        public IActionResult ListBooks()
        {
            return Ok(Array.Empty<string>());
        }

        [HttpPost()]
        public async Task<IActionResult> AddBook(CreateBookRequest request)
        {
            var command = _mapper.Map<CreateCommand>(request);
            ErrorOr<Book> createResult = await _mediator.Send(command);

            if(createResult.IsError && createResult.FirstError == Errors.Book.DuplicateBook)
            {
                return Problem(
                    statusCode: StatusCodes.Status409Conflict,
                    title: createResult.FirstError.Description);
            }

            return createResult.Match(
                createResult => CreatedAtRoute(createResult.Id ,_mapper.Map<BookResponse>(createResult)),
                errors => Problem(errors));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            ErrorOr<Book> getResult = await _mediator.Send(new BookQuery(id));

            if (getResult.IsError && getResult.FirstError == Errors.Book.BookNotFound)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: getResult.FirstError.Description);
            }

            return getResult.Match(
                createResult => Ok(_mapper.Map<BookResponse>(createResult)),
                errors => Problem(errors));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookRequest request)
        {
            if(id != request.Id)
            {
                return BadRequest();
            }

            var command = _mapper.Map<UpdateCommand>(request);
            ErrorOr<Book> updateResult = await _mediator.Send(command);

            if (updateResult.IsError && updateResult.FirstError == Errors.Book.BookNotFound)
            {
                return Problem(
                    statusCode: StatusCodes.Status404NotFound,
                    title: updateResult.FirstError.Description);
            }

            return updateResult.Match(
                updateResult => Ok(_mapper.Map<BookResponse>(updateResult)),
                errors => Problem(errors));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            ErrorOr<bool> deleteResult = await _mediator.Send(new DeleteCommand(id));

            if(deleteResult.IsError)
            {
                return Problem(deleteResult.Errors);
            }

            return NoContent();
        }
    }
}
