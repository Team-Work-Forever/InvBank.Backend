using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Authentication;

public class AuthenticationResult
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("refreshToken")]
    public string RefreshToken { get; set; } = null!;
}