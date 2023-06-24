using System.Net;
using System.Text.Json;
using Blazored.LocalStorage;
using InvBank.Web.Contracts.Authentication;
using InvBank.Web.Helper.Authentication;
using InvBank.Web.Provider.Authorization;

namespace InvBank.Web.Helper;

public class AuthApiHelper : IApiCaller
{
    private readonly ApiHelper _apiHelper;
    private readonly ILocalStorageService _localStorage;
    private readonly IJWTModifier _jwtModifier;

    public AuthApiHelper(
        ApiHelper apiHelper,
        ILocalStorageService localStorage,
        IJWTModifier jwtModifier)
    {
        _apiHelper = apiHelper;
        _localStorage = localStorage;
        _jwtModifier = jwtModifier;
    }

    private delegate Task<HttpResponseMessage> MakeCall();

    private async Task<IDictionary<string, string>> TryAddAuthorizationAsync(IDictionary<string, string>? headers)
    {

        var access_token = await _localStorage.GetItemAsStringAsync(CookieValue.AccessToken);
        var refresh_token = await _localStorage.GetItemAsStringAsync(CookieValue.RefreshToken);

        if (headers is null)
        {
            headers = new Dictionary<string, string>();
        }

        Dictionary<string, string> accessTokenClaims = _jwtModifier.GetClaims(access_token);
        var expireDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(accessTokenClaims["exp"])).UtcDateTime;

        if (expireDate.CompareTo(DateTime.Now) >= 0)
        {
            return headers;
        }

        var result = await _apiHelper.DoGet("/auth/refreshToken?token=" + refresh_token);

        if (result.StatusCode == HttpStatusCode.Unauthorized)
        {
            return headers;
        }

        var tokens = JsonSerializer.Deserialize<AuthenticationResult>(await result.Content.ReadAsStringAsync());

        if (tokens is null)
        {
            return headers;
        }

        access_token = tokens.AccessToken;
        refresh_token = tokens.RefreshToken;

        headers.Add("Authorization", access_token);
        return headers;

    }

    public async Task<HttpResponseMessage> DoDelete(string endpoint, IDictionary<string, string>? headers)
    {
        return await _apiHelper.DoDelete(endpoint, await TryAddAuthorizationAsync(headers));
    }

    public async Task<HttpResponseMessage> DoGet(string endpoint, IDictionary<string, string>? headers)
    {
        return await _apiHelper.DoGet(endpoint, await TryAddAuthorizationAsync(headers));
    }

    public async Task<HttpResponseMessage> DoPost(string endpoint, string payload, IDictionary<string, string>? headers)
    {
        return await _apiHelper.DoPost(endpoint, payload, await TryAddAuthorizationAsync(headers));
    }

    public async Task<HttpResponseMessage> DoPut(string endpoint, string payload, IDictionary<string, string>? headers)
    {
        return await _apiHelper.DoPut(endpoint, payload, await TryAddAuthorizationAsync(headers));
    }
}