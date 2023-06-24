using FluentValidation;
using InvBank.Backend.Application.Common;

namespace InvBank.Backend.Application.Validators;

public class MakeTransferValidation : AbstractValidator<MakeTransfer>
{
    public MakeTransferValidation()
    {
        RuleFor(request => request.AmountValue)
            .NotEqual(0)
            .WithMessage("Por favor indique um valor suprior a 0 u.m.");

        RuleFor(request => request.AmountValue)
            .GreaterThan(0)
            .WithMessage("Por favor indique um valor positivo");
    }
}