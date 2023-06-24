using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Account;

public class CreateAccountRequest
{   
    [JsonPropertyName("iban")]
    public string BankIban { get; set; } = null!;
}