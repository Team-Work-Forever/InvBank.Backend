using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.PropertyAccount;

public record PayPropertyRequest
{
    [JsonPropertyName("propertyId")]
    public Guid PropertyId { get; set; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}