using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryManager.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ApiController(ProblemDetailsFactory problemDatailsFactory)
    {
        _problemDetailsFactory = problemDatailsFactory;
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return ProblemWithExtensions(errors, statusCode: statusCode, title: firstError.Description);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }

    /// <summary>
    /// Creates an <see cref="ObjectResult"/> that produces a <see cref="ProblemDetails"/> response.
    /// </summary>
    /// <param name="statusCode">The value for <see cref="ProblemDetails.Status" />.</param>
    /// <param name="detail">The value for <see cref="ProblemDetails.Detail" />.</param>
    /// <param name="instance">The value for <see cref="ProblemDetails.Instance" />.</param>
    /// <param name="title">The value for <see cref="ProblemDetails.Title" />.</param>
    /// <param name="type">The value for <see cref="ProblemDetails.Type" />.</param>
    /// <returns>The created <see cref="ObjectResult"/> for the response.</returns>
    [NonAction]
    private ObjectResult ProblemWithExtensions(
        List<Error> erros,
        string? detail = null,
        string? instance = null,
        int? statusCode = null,
        string? title = null,
        string? type = null)
    {
        ProblemDetails problemDetails;
        if (_problemDetailsFactory == null)
        {
            // ProblemDetailsFactory may be null in unit testing scenarios. Improvise to make this more testable.
            problemDetails = new ProblemDetails
            {
                Detail = detail,
                Instance = instance,
                Status = statusCode ?? 500,
                Title = title,
                Type = type,
            };
        }
        else
        {
            problemDetails = _problemDetailsFactory.CreateProblemDetails(
                HttpContext,
                statusCode: statusCode ?? 500,
                title: title,
                type: type,
                detail: detail,
                instance: instance);
        }

        if(erros is not null)
        {
            problemDetails.Extensions.Add("errorCodes", erros.Select(e => e.Code));
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }
}
