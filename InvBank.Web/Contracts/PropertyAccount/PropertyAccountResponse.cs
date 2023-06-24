using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.PropertyAccount;

public class PropertyAccountResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

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

    [JsonPropertyName("yearluValue")]
    public decimal YearlyValue { get; set; }

    [JsonPropertyName("account")]
    public string Account { get; set; } = null!;

    public decimal Profit { get; set; }

}