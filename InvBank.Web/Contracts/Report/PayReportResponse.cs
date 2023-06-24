using System.Text.Json.Serialization;
using InvBank.Web.Contracts.Payment;

namespace InvBank.Web.Contracts.Report;

public class PayReportResponse
{
    [JsonPropertyName("paymentDeposit")]
    public IEnumerable<PayDepositResponse> PaymentDeposit { get; set; } = null!;

    [JsonPropertyName("paymentProperty")]
    public IEnumerable<PayPropertyResponse> PaymentProperty { get; set; } = null!;
}