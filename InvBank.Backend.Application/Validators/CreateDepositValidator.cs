using System.Globalization;
using FluentValidation;
using InvBank.Backend.Application.Actives.Deposit.CreateDeposit;
using InvBank.Backend.Application.Validators.Common;

namespace InvBank.Backend.Application.Validators;

public class CreateDepositValidator : AbstractValidator<CreateDepositCommand>
{
    public CreateDepositValidator()
    {

        RuleFor(request => request.InitialDate)
            .Must(ValidateUtils.MustBeDate)
            .WithMessage("Por favor indique uma data valida");

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