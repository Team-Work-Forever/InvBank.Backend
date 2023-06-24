using ErrorOr;
using FluentValidation;

namespace InvBank.Backend.Application.Services;

public class BaseService
{

    public async Task<ErrorOr<dynamic>> Validate<R>(IValidator<R> validator, R model)
    {

        var validationResult = await validator.ValidateAsync(model);

        if (validationResult.IsValid)
        {
            return 0;
        }

        var errors = validationResult
            .Errors
            .ConvertAll(error => Error.Validation(
                error.PropertyName,
                error.ErrorMessage
            ));

        return (dynamic)errors;

    }

}