using FluentValidation;
using InvBank.Backend.Contracts.Fund;

namespace InvBank.Backend.Application.Validators;

public class AssociateAccountToFundRequestValidator : AbstractValidator<AssociateAccountToFundRequest>
{
    public AssociateAccountToFundRequestValidator()
    {
        RuleFor(request => request.IBAN).Length(30)
                 .WithMessage("Por favor indique um IBAN com 30 dígitos");
        RuleFor(request => request.Amount)
                  .NotEqual(0)
                  .WithMessage("Por favor indique uma valor maior que 0 u.m");
        RuleFor(request => request.Amount)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");
    }
}