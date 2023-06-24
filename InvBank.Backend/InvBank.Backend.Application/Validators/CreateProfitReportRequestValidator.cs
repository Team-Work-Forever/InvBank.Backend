using FluentValidation;
using InvBank.Backend.Application.Validators.Common;
using InvBank.Backend.Contracts.Report;

namespace InvBank.Backend.Application.Validators;

public class CreateProfitReportRequestValidator : AbstractValidator<CreateProfitReportRequest>
{
    public CreateProfitReportRequestValidator()
    {
        RuleFor(request => request.InitialDate.ToString())
                          .Must(ValidateUtils.MustBeDate)
                          .WithMessage("Por favor indique uma data valida");

        RuleFor(request => request.EndDate.ToString())
                   .Must(ValidateUtils.MustBeDate)
                   .WithMessage("Por favor indique uma data valida");
    }
}