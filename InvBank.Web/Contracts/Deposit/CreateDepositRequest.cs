using System.Text.Json.Serialization;

namespace InvBank.Web.Helper.EndPoints;

public class CreateDepositRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("initialDate")]
    public string InitialDate { get; set; } = null!;

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("taxPercent")]
    public decimal TaxPercent { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [JsonPropertyName("yearlyTax")]
    public decimal YearlyTax { get; set; }

    [JsonPropertyName("iban")]
    public string IBAN { get; set; } = null!;
}