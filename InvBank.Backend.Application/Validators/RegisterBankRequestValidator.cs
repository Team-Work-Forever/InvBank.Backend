using FluentValidation;
using InvBank.Backend.Contracts.Bank;

namespace InvBank.Backend.Application.Validators;

public class RegisterBankRequestValidator : AbstractValidator<RegisterBankRequest>
{

    public RegisterBankRequestValidator()
    {
        RuleFor(request => request.Iban).Length(28)
            .WithMessage("Por favor indique um IBAN com 28 dígitos");
        RuleFor(request => request.Phone)
            .Length(9)
            .Matches("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$")
            .WithMessage("Por favor indique um número de telefone válido");
        RuleFor(request => request.PostalCode)
            .Length(8)
            .Matches("^\\d{4}-\\d{3}$")
            .WithMessage("Por favor indique um codigo postal válido");
    }

}