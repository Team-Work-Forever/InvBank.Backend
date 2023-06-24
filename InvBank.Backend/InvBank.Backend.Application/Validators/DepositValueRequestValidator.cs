using FluentValidation;
using InvBank.Backend.Contracts.Deposit;

namespace InvBank.Backend.Application.Validators;

public class DepositValueRequestValidator : AbstractValidator<DepositValueRequest>
{
    public DepositValueRequestValidator()
    {
        RuleFor(request => request.AmountValue)
                  .NotEqual(0)
                  .WithMessage("Por favor indique uma valor maior que 0 u.m");

        RuleFor(request => request.AmountValue)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");
    }
}