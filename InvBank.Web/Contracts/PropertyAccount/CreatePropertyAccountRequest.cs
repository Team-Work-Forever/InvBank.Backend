using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.PropertyAccount;

public class CreatePropertyAccountRequest
{

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("initialDate")]
    public string InitialDate { get; set; } = null!;

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

    [JsonPropertyName("AccountIban")]
    public string AccountIBAN { get; set; } = null!;
}