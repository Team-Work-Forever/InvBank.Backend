using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.PropertyAccount;

public class UpdatePropertyAccountRequest
{

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("taxPercent")]
    public decimal TaxPercent { get; set; }

    [JsonPropertyName("designation")]
    public string Designation { get; set; } = null!;

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; } = null!;

    [JsonPropertyName("propertyValue")]
    public decimal PropertyValue { get; set; }

    [JsonPropertyName("rentValue")]
    public decimal RentValue { get; set; }

    [JsonPropertyName("monthValue")]
    public decimal MonthValue { get; set; }

    [JsonPropertyName("yearlyValue")]
    public decimal YearlyValue { get; set; }

}