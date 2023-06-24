using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Authentication;

public class RegisterCompanyRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = null!;

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = null!;

    [JsonPropertyName("nif")]
    public string Nif { get; set; } = null!;
}