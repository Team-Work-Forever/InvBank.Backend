using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts.Report;

namespace InvBank.Web.Helper.EndPoints;

public class ReportEndPoint : BaseEndPoint
{
    public ReportEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<ProfitReportResponse>> GenerateReportProfit(GenerateProfitReportRequest request)
    {
        return await MakeRequest<ProfitReportResponse>
        (
            () => _apiHelper.DoPost("/report/profit", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<PayReportResponse>> GenerateReportPay(string accountIban, GeneratePayReportRequest request)
    {
        return await MakeRequest<PayReportResponse>
        (
            () => _apiHelper.DoPost($"/report/pay?iban={accountIban}", JsonSerializer.Serialize(request))
        );
    }

}