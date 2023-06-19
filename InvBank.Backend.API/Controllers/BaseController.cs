using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace InvBank.Backend.API.Controllers;

public class BaseController : ControllerBase
{

    public IActionResult Problem(List<Error> errors)
    {
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