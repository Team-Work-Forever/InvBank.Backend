using FluentValidation;
using InvBank.Backend.Application.Validators.Common;
using InvBank.Backend.Contracts.Fund;

namespace InvBank.Backend.Application.Validators;

public class CreateFundRequestValidator : AbstractValidator<CreateFundRequest>
{
    public CreateFundRequestValidator()
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

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Por favor indique um nome para o deposito");
    }
}