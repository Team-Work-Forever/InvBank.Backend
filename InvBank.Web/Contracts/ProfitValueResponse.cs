using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts;

public class ProfitValueResponse
{
    [JsonPropertyName("profit")]
    public decimal Profit { get; set; }
}