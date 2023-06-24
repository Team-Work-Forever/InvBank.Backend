using System.Security.Claims;
using ErrorOr;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Domain.Errors;
using Microsoft.AspNetCore.Http;

namespace InvBank.Backend.Infrastructure.Authentication;

public class AuthorizationFacade : IAuthorizationFacade
{

    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationFacade(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Auth>> GetAuthenticatedUser()
    {

        Claim? claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email);

        if (claim is null)
        {
            return Errors.Auth.CannotGetEmail;
        }

        Auth? userAuth = await _userRepository.GetUserAuth(claim.Value);

        if (userAuth is null)
        {
            return Errors.Auth.AuthorizationFailed;
        }

        return userAuth;

    }

}