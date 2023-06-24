using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Fund;

public record AssociateAccountToFundRequest
{
    [JsonPropertyName("iban")]
    public string IBAN { get; set; } = string.Empty;

    [JsonPropertyName("fundId")]
    public Guid FundId { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}