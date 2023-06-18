namespace InvBank.Backend.Contracts.Authentication;

public class AuthenticationResult
{
    public AuthenticationResult(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }


}