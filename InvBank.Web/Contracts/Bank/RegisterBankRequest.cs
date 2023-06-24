using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Bank;

public class RegisterBankRequest
{

    [JsonPropertyName("iban")]
    public string Iban { get; set; } = null!;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = null!;

    [JsonPropertyName("postalCode")]
    public string postalCode { get; set; } = null!;

}