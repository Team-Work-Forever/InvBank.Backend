using FluentValidation;
using InvBank.Backend.Application.Validators.Common;
using InvBank.Backend.Contracts.Report;

namespace InvBank.Backend.Application.Validators;

public class CreatePayReportCommandValidator : AbstractValidator<CreatePayReportCommand>
{
    public CreatePayReportCommandValidator()
    {
        RuleFor(request => request.InitialDate.ToString())
                   .Must(ValidateUtils.MustBeDate)
                   .WithMessage("Por favor indique uma data valida");

        RuleFor(request => request.EndDate.ToString())
                   .Must(ValidateUtils.MustBeDate)
                   .WithMessage("Por favor indique uma data valida");

        RuleFor(request => request.Iban).Length(30)
                 .WithMessage("Por favor indique um IBAN com 30 dígitos");
    }
}