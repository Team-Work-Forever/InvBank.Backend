using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts.Report;

public class ProfitReportResponse
{
    [JsonPropertyName("beforeProfitDeposit")]
    public decimal BeforeProfitDeposit { get; set; }

    [JsonPropertyName("afterProfitDeposit")]
    public decimal AfterProfitDeposit { get; set; }

    [JsonPropertyName("beforeProfitProperty")]
    public decimal BeforeProfitProperty { get; set; }

    [JsonPropertyName("afterProfitProperty")]
    public decimal AfterProfitProperty { get; set; }

    [JsonPropertyName("profitMean")]
    public decimal ProfitMean { get; set; }
}