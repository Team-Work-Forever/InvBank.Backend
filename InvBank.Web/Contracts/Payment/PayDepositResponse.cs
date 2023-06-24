using System.Text.Json.Serialization;
using InvBank.Web.Contracts.Deposit;

namespace InvBank.Web.Contracts.Payment;

public record PayDepositResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("paymentDate")]
    public string PaymentDate { get; set; } = null!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("depositAccount")]
    public DepositResponse DepositAccount { get; set; } = null!;
};