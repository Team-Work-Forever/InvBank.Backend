using FluentValidation;
using InvBank.Backend.Contracts.Account;

namespace InvBank.Backend.Application.Validators;

public class CreateAccountRequestValidator : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
        RuleFor(request => request.iban)
            .Length(30)
            .WithMessage("Por favor indique um IBAN com 30 dígitos");
    }
}