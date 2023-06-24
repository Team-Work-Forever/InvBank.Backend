using ErrorOr;
using FluentValidation;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Application.Services;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Commands.RegisterCompany;

public class RegisterCompanyCommandHandler : BaseService, IRequestHandler<RegisterCompanyCommand, ErrorOr<string>>
{
    private readonly IValidator<RegisterCompanyCommand> _validator;
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IUserRepository _userRepository;

    public RegisterCompanyCommandHandler(
        IUserRepository userRepository,
        IPasswordEncoder passwordEncoder,
        IValidator<RegisterCompanyCommand> validator)
    {
        _userRepository = userRepository;
        _passwordEncoder = passwordEncoder;
        _validator = validator;
    }

    public async Task<ErrorOr<string>> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
    {

        var validationResult = await Validate<RegisterCompanyCommand>(_validator, request);

        if (validationResult.IsError)
        {
            return validationResult.Errors;
        }

        var authCompany = await _userRepository.GetUserAuth(request.Email);

        if (authCompany is not null)
        {
            return Errors.Auth.DuplicatedEmail;
        }

        await _userRepository.CreateUser(
         new Auth
         {
             Email = request.Email,
             Company = new Company
             {
                 CompanyName = request.Name,
                 Phone = request.Phone,
                 Nif = request.Nif,
                 PostalCode = request.PostalCode
             },
             UserPassword = _passwordEncoder.Encode(request.Password),
         });

        return "";

    }

}