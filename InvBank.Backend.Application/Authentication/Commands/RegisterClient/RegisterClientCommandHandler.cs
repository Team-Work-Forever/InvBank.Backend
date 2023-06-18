using ErrorOr;
using InvBank.Backend.Application.Common.Errors;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Commands.RegisterClient;

public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IDateFormatter _dateFormatterProvider;
    private readonly IUserRepository _userRepository;

    public RegisterClientCommandHandler(IUserRepository userRepository, IDateFormatter dateFormatterProvider, IPasswordEncoder passwordEncoder)
    {
        _userRepository = userRepository;
        _dateFormatterProvider = dateFormatterProvider;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        var authUser = await _userRepository.GetUserAuth(request.Email);

        if (authUser is not null)
        {
            return Errors.Auth.DuplicatedEmail;
        }

        await _userRepository.CreateUser(
            new Auth
            {
                Email = request.Email,
                Profile = new Profile
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    BirthDate = _dateFormatterProvider.ConvertToDateTime(request.BirthDate),
                    Nif = request.Nif,
                    Cc = request.Cc,
                    Phone = request.Phone,
                    PostalCode = request.PostalCode,
                },
                UserPassword = _passwordEncoder.Encode(request.Password),
            });

        return new AuthenticationResult("adsdasd", "dasd");

    }

}
