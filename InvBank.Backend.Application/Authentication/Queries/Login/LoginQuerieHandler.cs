using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Queries.Login;

public class LoginQuerieHandler : IRequestHandler<LoginQuerie, ErrorOr<AuthenticationResult>>
{
    private readonly IPasswordEncoder _passwordEncoder;
    private readonly IJWTModifier _jwtModifier;
    private readonly IUserRepository _userRepository;

    public LoginQuerieHandler(IUserRepository userRepository, IJWTModifier jwtModifier, IPasswordEncoder passwordEncoder)
    {
        _userRepository = userRepository;
        _jwtModifier = jwtModifier;
        _passwordEncoder = passwordEncoder;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuerie request, CancellationToken cancellationToken)
    {

        // Verify user
        Auth? userAuth = await _userRepository.GetUserAuth(request.Email);

        if (userAuth is null)
        {
            return Errors.Auth.IncorrentCredentials;
        }

        if (!_passwordEncoder.VerifyHash(userAuth.UserPassword, request.Password))
        {
            return Errors.Auth.IncorrentCredentials;
        }

        // Generate JWT Tokens
        string accessToken = _jwtModifier.GenerateToken(new Dictionary<string, string>(){
            { "email", userAuth.Email },
            { "role", userAuth.UserRole.ToString() },
            { "userId", userAuth.Id.ToString() }
         }, DateTime.Now.AddHours(2));

        string refreshToken = _jwtModifier.GenerateToken(new Dictionary<string, string>(){
            { "email", userAuth.Email },
            { "role", userAuth.UserRole.ToString() },
            { "userId", userAuth.Id.ToString() }
         }, DateTime.Now.AddDays(3));

        return new AuthenticationResult(
            accessToken,
            refreshToken
        );

    }

}