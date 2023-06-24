using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Deposit;

public class DepositResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("depositName")]
    public string DepositName { get; set; } = null!;

    [JsonPropertyName("initialDate")]
    public string InitialDate { get; set; } = null!;

    [JsonPropertyName("duration")]
    public int Duration { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [JsonPropertyName("taxPercent")]
    public decimal TaxPercent { get; set; }

    [JsonPropertyName("yearlyTax")]
    public decimal YearlyTax { get; set; }

    [JsonPropertyName("account")]
    public string Account { get; set; } = null!;
};