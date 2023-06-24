using FluentValidation;
using InvBank.Backend.Application.Authentication.Queries.Login;

namespace InvBank.Backend.Application.Validators;

public class LoginQuerieValidator : AbstractValidator<LoginQuerie>
{
    public LoginQuerieValidator()
    {
        RuleFor(request => request.Email)
            .EmailAddress()
            .WithMessage("Por favor indique um email valido");

        RuleFor(request => request.Password.Length)
            .InclusiveBetween(6, 16)
            .WithMessage("Por favor indique uma palavra-passe entre 6 a 16 caracteres");
    }
}