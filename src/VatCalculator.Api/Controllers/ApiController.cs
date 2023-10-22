namespace VatCalculator.Api.Controllers;

using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VatCalculator.Api.Common.Errors;
using VatCalculator.Api.Common.Http;


[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<IError> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.HasMetadataKey(ErrorMetadata.Validation)))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        return Problem(errors[0]);
    }

    private IActionResult Problem(IError error)
    {
        var statusCode = error switch
        {
            var _ when error.Metadata.ContainsValue(ErrorMetadata.Conflict) => StatusCodes.Status409Conflict,
            var _ when error.Metadata.ContainsValue(ErrorMetadata.Validation) => StatusCodes.Status400BadRequest,
            var _ when error.Metadata.ContainsValue(ErrorMetadata.NotFound) => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Message);
    }

    private IActionResult ValidationProblem(List<IError> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Message,
                error.Message);
        }

        return ValidationProblem(modelStateDictionary);
    }

    protected IActionResult ReturnResult<T>(Result<T> result)
    {
        return result.IsSuccess ? new OkObjectResult(result.Value) : GetError(result.ToResult());
    }

    private IActionResult GetError(Result result)
    {
        return Problem(result.Errors);
    }
}