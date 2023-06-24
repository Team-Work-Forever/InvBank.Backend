using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InvBank.Backend.Infrastructure.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class AuthorizeRole : AuthorizeAttribute, IAuthorizationFilter
{

    private readonly IEnumerable<string> _roles;

    public AuthorizeRole(params Role[] roles)
    {
        _roles = roles.Select(role => ((int)role).ToString());
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {

        var jwtModifier = context.HttpContext.RequestServices.GetService(typeof(IJWTModifier)) as IJWTModifier;

        if (jwtModifier is null)
        {
            UnauthorizedResponseAsync(context);
            return;
        }

        string authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            UnauthorizedResponseAsync(context);
            return;
        }

        Dictionary<string, string> claims = jwtModifier.GetClaims(authorizationHeader.Replace("Bearer ", string.Empty));

        if (!claims.TryGetValue("role", out var role))
        {
            UnauthorizedResponseAsync(context);
            return;
        }

        if (!_roles.Contains(role))
        {
            UnauthorizedResponseAsync(context);
            return;
        }

    }

    private void UnauthorizedResponseAsync(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedResult();
    }

}