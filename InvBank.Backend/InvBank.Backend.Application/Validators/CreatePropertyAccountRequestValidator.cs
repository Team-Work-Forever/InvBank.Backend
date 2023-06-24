using FluentValidation;
using InvBank.Backend.Application.Validators.Common;
using InvBank.Backend.Contracts.Account;
using InvBank.Backend.Contracts.PropertyAccount;

namespace InvBank.Backend.Application.Validators;

public class CreatePropertyAccountRequestValidator : AbstractValidator<CreatePropertyAccountRequest>
{
    public CreatePropertyAccountRequestValidator()
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

        RuleFor(request => request.PropertyValue)
          .NotEqual(0)
          .WithMessage("Por favor indique uma valor maior que 0 u.m");

        RuleFor(request => request.PropertyValue)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");

        RuleFor(request => request.RentValue)
          .NotEqual(0)
          .WithMessage("Por favor indique uma valor maior que 0 u.m");

        RuleFor(request => request.RentValue)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");

        RuleFor(request => request.MonthValue)
          .NotEqual(0)
          .WithMessage("Por favor indique uma valor maior que 0 u.m");

        RuleFor(request => request.MonthValue)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");

        RuleFor(request => request.YearlyValue)
          .NotEqual(0)
          .WithMessage("Por favor indique uma valor maior que 0 u.m");

        RuleFor(request => request.YearlyValue)
            .GreaterThan(0)
            .WithMessage("Por favor indique uma valor positivo");

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Por favor indique um nome para o ímovel");

        RuleFor(request => request.Designation)
            .NotEmpty()
            .WithMessage("Por favor indique uma designação para o ímovel");

        RuleFor(request => request.PostalCode)
            .Length(8)
            .Matches("^\\d{4}-\\d{3}$")
            .WithMessage("Por favor indique um codigo postal válido");

        RuleFor(request => request.AccountIBAN).Length(30)
           .WithMessage("Por favor indique um IBAN com 30 dígitos");
    }
}