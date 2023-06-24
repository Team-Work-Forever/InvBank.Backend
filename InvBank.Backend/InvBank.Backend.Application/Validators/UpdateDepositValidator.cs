using FluentValidation;
using InvBank.Backend.Contracts.Deposit;

namespace InvBank.Backend.Application.Validators;

public class UpdateDepositValidator : AbstractValidator<UpdateDepositRequest>
{
    public UpdateDepositValidator()
    {
        RuleFor(request => request.Duration)
            .NotEqual(0)
            .WithMessage("Por favor indique uma duração maior que 0 meses");

        RuleFor(request => request.Duration)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma duração positiva");

        RuleFor(request => request.TaxPercent)
           .NotEqual(0)
           .WithMessage("Por favor indique uma taxa diferente de 0%");

        RuleFor(request => request.Value)
          .NotEqual(0)
          .WithMessage("Por favor indique uma valor maior que 0 u.m");

        RuleFor(request => request.Value)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");

        RuleFor(request => request.YearlyTax)
            .NotEqual(0)
            .WithMessage("Por favor indique uma taxa diferente de 0%");

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Por favor indique um nome para o deposito");
    }
}