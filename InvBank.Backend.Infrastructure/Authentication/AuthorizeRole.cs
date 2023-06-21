using InvBank.Backend.Application.Common.Providers;
using InvBank.Backend.Contracts;
using InvBank.Backend.Domain.Entities;
using InvBank.Backend.Infrastructure.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

    public async void OnAuthorization(AuthorizationFilterContext context)
    {

        var jwtModifier = context.HttpContext.RequestServices.GetService(typeof(IJWTModifier)) as IJWTModifier;

        if (jwtModifier is null)
        {
            await UnauthorizedResponseAsync(context);
            return;
        }

        string authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            await UnauthorizedResponseAsync(context);
            return;
        }

        Dictionary<string, string> claims = jwtModifier.GetClaims(authorizationHeader.Replace("Bearer ", string.Empty));

        if (!claims.TryGetValue("role", out var role))
        {
            await UnauthorizedResponseAsync(context);
            return;
        }

        if (!_roles.Contains(role))
        {
            await UnauthorizedResponseAsync(context);
            return;
        }

    }

    private async Task UnauthorizedResponseAsync(AuthorizationFilterContext context)
    {
        context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
        await context.HttpContext.Response.WriteAsJsonAsync(new SimpleResponse("Não tens permissão para executar esta ação"));
    }

}