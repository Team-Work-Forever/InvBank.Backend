using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts.Authentication;
using InvBank.Backend.Domain.Errors;
using MediatR;

namespace InvBank.Backend.Application.Authentication.Queries.TokenQuerie;

public class TokenQuerieHandler : IRequestHandler<TokenQuerie, ErrorOr<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJWTModifier _jwtModifier;

    public TokenQuerieHandler(IJWTModifier jwtModifier, IUserRepository userRepository)
    {
        _jwtModifier = jwtModifier;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(TokenQuerie request, CancellationToken cancellationToken)
    {

        bool isValidToken = await _jwtModifier.ValidateTokenAsync(request.RefreshToken);

        if (!isValidToken)
        {
            return Error.Unexpected("Não estás autorizado a prosseguir");
        }

        Dictionary<string, string> claims = _jwtModifier.GetClaims(request.RefreshToken);

        var userAuth = await _userRepository.GetUserAuth(claims["email"]);

        if (userAuth is null)
        {
            return Errors.Auth.AuthorizationFailed;
        }

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