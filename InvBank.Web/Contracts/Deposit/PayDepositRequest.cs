using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Deposit;

public record PayDepositRequest
{
    [JsonPropertyName("depositId")]
    public Guid DepositId { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}