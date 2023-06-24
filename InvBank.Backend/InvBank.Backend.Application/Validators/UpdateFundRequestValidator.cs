using FluentValidation;
using InvBank.Backend.Contracts.Fund;

namespace InvBank.Backend.Application.Validators;

public class UpdateFundRequestValidator : AbstractValidator<UpdateFundRequest>
{
    public UpdateFundRequestValidator()
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

        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage("Por favor indique um nome para o deposito");
    }
}