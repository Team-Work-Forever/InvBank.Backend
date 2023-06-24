using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Account;

public class MakeTransferRequest
{
    [JsonPropertyName("amountValue")]
    public decimal AmountValue { get; set; }
}