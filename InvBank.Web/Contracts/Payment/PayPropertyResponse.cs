using System.Text.Json.Serialization;
using InvBank.Web.Contracts.PropertyAccount;

namespace InvBank.Web.Contracts.Payment;

public class PayPropertyResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("paymentDate")]
    public string PaymentDate { get; set; } = null!;

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("depositAccount")]
    public PropertyAccountResponse DepositAccount { get; set; } = null!;
};