using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Authentication;

public class RegisterClientRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("birthDate")]
    public string BirthDate { get; set; } = null!;

    [JsonPropertyName("nif")]
    public string Nif { get; set; } = null!;

    [JsonPropertyName("cc")]
    public string Cc { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = null!;

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = null!;
}