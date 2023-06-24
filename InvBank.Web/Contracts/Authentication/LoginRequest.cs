using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Authentication;

public class LoginRequest
{

    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

}