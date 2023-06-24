using FluentValidation;
using InvBank.Backend.Application.Validators.Common;
using InvBank.Backend.Contracts.User;

namespace InvBank.Backend.Application.Validators;

public class UpdateUserByRoleRequestValidator : AbstractValidator<UpdateUserByRoleRequest>
{
    public UpdateUserByRoleRequestValidator()
    {
         RuleFor(request => request.Email)
            .EmailAddress()
            .WithMessage("Por favor indique um email valido");

        RuleFor(request => request.BirthDate)
           .Must(ValidateUtils.MustBeDate)
           .WithMessage("Por favor indique uma data de nascimento valida");

        RuleFor(request => request.FirstName)
           .NotEmpty()
           .WithMessage("Por favor indique o seu nome própio");

        RuleFor(request => request.LastName)
           .NotEmpty()
           .WithMessage("Por favor indique o seu último nome");

        RuleFor(request => request.Nif)
            .Length(9)
            .WithMessage("Por favor indique um nif válido");

        RuleFor(request => request.Cc)
            .Length(8)
            .WithMessage("Por favor indique um cc válido");

        RuleFor(request => request.PostalCode)
            .Length(8)
            .Matches("^\\d{4}-\\d{3}$")
            .WithMessage("Por favor indique um codigo postal válido");

        RuleFor(request => request.Phone)
            .Length(9)
            .WithMessage("Por favor indique um número de telefone válido");
    }
}