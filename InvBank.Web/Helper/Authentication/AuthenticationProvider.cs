using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using InvBank.Web.Provider.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace InvBank.Web.Helper.Authentication;

public class AuthenticationProvider : AuthenticationStateProvider
{
    private readonly IJWTModifier _jwtModifier;
    private readonly ILocalStorageService _localStorage;
    private readonly HttpClient _httpClient;

    public AuthenticationProvider(HttpClient httpClient, ILocalStorageService localStorage, IJWTModifier jwtModifier)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _jwtModifier = jwtModifier;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        string accessToken = await _localStorage.GetItemAsStringAsync(CookieValue.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        Dictionary<string, string> dictionary = _jwtModifier.GetClaims(accessToken);
        IEnumerable<Claim> claims = dictionary.Select(value => new Claim(value.Key, value.Value));

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));

    }

    public async Task LogOut() 
    {
        await _localStorage.RemoveItemAsync(CookieValue.AccessToken);
        await _localStorage.RemoveItemAsync(CookieValue.RefreshToken);
    }

    public async Task AuthenticateUser(string accessToken, string refreshToken)
    {
        await _localStorage.SetItemAsStringAsync(CookieValue.AccessToken, accessToken);
        await _localStorage.SetItemAsStringAsync(CookieValue.RefreshToken, refreshToken);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim("email", "email") }, "jwt")))));
    }

}