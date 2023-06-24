using FluentValidation;
using InvBank.Backend.Application.Authentication.Commands.RegisterCompany;

namespace InvBank.Backend.Application.Validators;

public class RegisterCompanyCommandValidator : AbstractValidator<RegisterCompanyCommand>
{
    public RegisterCompanyCommandValidator()
    {
        RuleFor(request => request.Email)
            .EmailAddress()
            .WithMessage("Por favor indique um email valido");

        RuleFor(request => request.Name)
           .NotEmpty()
           .WithMessage("Por favor indique o nome da empresa");

        RuleFor(request => request.Nif)
            .Length(9)
            .WithMessage("Por favor indique um nif válido");

        RuleFor(request => request.PostalCode)
            .Length(8)
            .Matches("^\\d{4}-\\d{3}$")
            .WithMessage("Por favor indique um codigo postal válido");

        RuleFor(request => request.Phone)
            .Length(9)
            .WithMessage("Por favor indique um número de telefone válido");

        RuleFor(request => request.Password.Length)
            .InclusiveBetween(6, 16)
            .WithMessage("Por favor indique uma palavra-passe entre 6 a 16 caracteres");
    }
}