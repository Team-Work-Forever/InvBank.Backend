using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Fund;

public class CreateFundRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("initialDate")]
    public string InitialDate { get; set; } = null!;

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("taxPercent")]
    public decimal TaxPercent { get; set; }
}