using Blazored.LocalStorage;
using ErrorOr;
using InvBank.Models.auth;
using InvBank.Web.Contracts.Authentication;
using InvBank.Web.Helper.Authentication;
using InvBank.Web.Helper.EndPoints;
using Microsoft.AspNetCore.Components.Authorization;

namespace InvBank.Service;

public class AuthenticationService
{
    private readonly AuthenticationProvider _authenticationProvider;
    private readonly AuthenticationEndPoint _authenticationEndPoint;

    public AuthenticationService(AuthenticationEndPoint authenticationEndPoint, AuthenticationStateProvider authenticationProvider)
    {
        _authenticationEndPoint = authenticationEndPoint;
        _authenticationProvider = (AuthenticationProvider)authenticationProvider;
    }

    public async Task<ErrorOr<bool>> Login(string email, string password)
    {
        var authResult = await _authenticationEndPoint.Login(new LoginRequest
        {
            Email = email,
            Password = password
        });

        if (authResult.IsError)
        {
            return authResult.Errors;
        }

        await _authenticationProvider.AuthenticateUser(authResult.Value.AccessToken, authResult.Value.RefreshToken);

        return true;
    }

    public async Task<ErrorOr<bool>> RegisterClient(RegisterClientModel model)
    {
        var authResult = await _authenticationEndPoint.RegisterClient(new RegisterClientRequest
        {
            Email = model.Email,
            Password = model.Password,
            BirthDate = DateOnly.FromDateTime(model.BirthDate).ToString("dd/MM/yyyy"),
            FirstName = model.FirstName,
            LastName = model.LastName,
            Cc = model.Cc,
            Phone = model.Phone,
            Nif = model.Nif,
            PostalCode = model.PostalCode
        });

        if (authResult.IsError)
        {
            return authResult.Errors;
        }

        return true;

    }

    public async Task<ErrorOr<bool>> RegisterCompany(RegisterCompanyModel model)
    {
        var authResult = await _authenticationEndPoint.RegisterCompany(new RegisterCompanyRequest
        {
            Email = model.Email,
            Password = model.Password,
            Name = model.Name,
            Phone = model.Phone,
            Nif = model.Nif,
            PostalCode = model.PostalCode
        });

        if (authResult.IsError)
        {
            return authResult.Errors;
        }

        return true;

    }

}