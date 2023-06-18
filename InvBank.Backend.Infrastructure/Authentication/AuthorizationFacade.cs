using System.Security.Claims;
using InvBank.Backend.Application.Common.Interfaces;
using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Domain.Entities;
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

    public async Task<Auth> GetAuthenticatedUser()
    {

        Claim? claim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email);

        if (claim is null)
        {
            throw new Exception("");
        }

        Auth? userAuth = await _userRepository.GetUserAuth(claim.Value);

        if (userAuth is null)
        {
            throw new Exception("Não existe nenhum utilizador com esse email");
        }

        return userAuth;

    }

}