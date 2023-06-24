using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.Account;
using InvBank.Web.Contracts.Fund;

namespace InvBank.Web.Helper.EndPoints;

public class FundEndPoint : BaseEndPoint
{
    public FundEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<ProfitValueResponse>> GetProfit(Guid fundId)
    {
        return await MakeRequest<ProfitValueResponse>(
            () => _apiHelper.DoGet($"/funds/profit?id={fundId}")
        );
    }

    public async Task<ErrorOr<FundResponse>> GetFund(Guid fundId)
    {
        return await MakeRequest<FundResponse>(
            () => _apiHelper.DoGet($"/funds?fundId={fundId}")
        );
    }

    public async Task<ErrorOr<IEnumerable<FundResponse>>> GetAllFunds()
    {
        return await MakeRequest<IEnumerable<FundResponse>>(
            () => _apiHelper.DoGet($"/funds/all")
        );
    }

    public async Task<ErrorOr<FundResponse>> AssociateAccountToFund(AssociateAccountToFundRequest request)
    {
        return await MakeRequest<FundResponse>(
            () => _apiHelper.DoPost($"/funds/associate", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> InvestOnFund(string iban, Guid fundId, MakeTransferRequest request)
    {
        return await MakeRequest<SimpleResponse>(
            () => _apiHelper.DoPost($"/funds/invest?iban={iban}&fundId={fundId}", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> CreateFund(CreateFundRequest request)
    {
        return await MakeRequest<SimpleResponse>(
            () => _apiHelper.DoPost($"/funds/create", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<IEnumerable<FundResponse>>> GetInvestFundOfAccount(string accountIban)
    {
        return await MakeRequest<IEnumerable<FundResponse>>(
            () => _apiHelper.DoGet($"/funds/account?iban={accountIban}")
        );
    }

}