using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InvBank.Backend.API.Controllers;

public class BaseController : ControllerBase
{

    public ActionResult<T> Problem<T>(List<Error> errors)
    {

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            var modelDictionary = new ModelStateDictionary();

            foreach (var err in errors)
            {
                modelDictionary.AddModelError(
                    err.Code,
                    err.Description
                );
            }

            return ValidationProblem(modelDictionary);
        }

        var error = errors[0];

        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unexpected => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: error.Description);

    }

}