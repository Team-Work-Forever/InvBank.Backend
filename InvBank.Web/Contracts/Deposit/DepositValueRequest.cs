using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Deposit;

public class DepositValueRequest
{
    [JsonPropertyName("amountValue")]
    public decimal AmountValue { get; set; }
}