using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Report;

public class GenerateProfitReportRequest
{
    [JsonPropertyName("initialDate")]
    public string InitialDate { get; set; } = null!;

    [JsonPropertyName("endDate")]
    public string EndDate { get; set; } = null!;
}