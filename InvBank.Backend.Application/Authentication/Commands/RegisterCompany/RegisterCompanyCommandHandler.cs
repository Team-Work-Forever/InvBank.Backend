using ErrorOr;
using InvBank.Backend.Application.Common.Errors;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Commands.RegisterCompany;

public class RegisterCompanyCommandHandler : IRequestHandler<RegisterCompanyCommand, ErrorOr<string>>
{
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IUserRepository _userRepository;

    public RegisterCompanyCommandHandler(IUserRepository userRepository, IPasswordEncoder passwordEncoder)
    {
        _userRepository = userRepository;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<ErrorOr<string>> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
    {

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