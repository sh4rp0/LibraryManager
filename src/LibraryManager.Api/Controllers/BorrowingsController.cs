using ErrorOr;
using LibraryManager.Application.BookBorrowing.Commands;
using LibraryManager.Contracts.Borrowings;
using LibraryManager.Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LibraryManager.Api.Controllers;

[Route("borrowings")]
public class BorrowingsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public BorrowingsController(IMediator mediator, IMapper mapper, ProblemDetailsFactory problemDetailsFactory) : base(problemDetailsFactory)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost()]
    public async Task<IActionResult> BorrowBook(BorrowingRequest request)
    {
        var command = _mapper.Map<BorrowCommand>(request);
        ErrorOr<Borrowing> borrowResult = await _mediator.Send(command);

        if (borrowResult.IsError && borrowResult.FirstError.Type == ErrorType.NotFound)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: borrowResult.FirstError.Description);
        }

        return borrowResult.Match(
            createResult => CreatedAtRoute(createResult.Id, _mapper.Map<BorrowingResponse>(createResult)),
            errors => Problem(errors));
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> ReturnBook(int id, ReturnRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        var command = _mapper.Map<ReturnCommand>(request);
        ErrorOr<Borrowing> returnResult = await _mediator.Send(command);

        if (returnResult.IsError && returnResult.FirstError.Type == ErrorType.NotFound)
        {
            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: returnResult.FirstError.Description);
        }

        return returnResult.Match(
            createResult => Ok(_mapper.Map<BorrowingResponse>(createResult)),
            errors => Problem(errors));
    }
}
