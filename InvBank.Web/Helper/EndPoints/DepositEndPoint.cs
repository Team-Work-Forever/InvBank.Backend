using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.Deposit;

namespace InvBank.Web.Helper.EndPoints;

public class DepositEndPoint : BaseEndPoint
{
    public DepositEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<ProfitValueResponse>> GetProfit(Guid depositId)
    {
        return await MakeRequest<ProfitValueResponse>
        (
            () => _apiHelper.DoGet($"/deposits/profit?depositId=" + depositId)
        );
    }

    public async Task<ErrorOr<SimpleResponse>> SetDepositValue(Guid depositId, DepositValueRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost($"/deposits/set/money?depositId=" + depositId, JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> GetDepositValue(Guid depositId, DepositValueRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost($"/deposits/get/money?depositId=" + depositId, JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> PayDeposit(PayDepositRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost($"/deposits/pay", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<DepositResponse>> GetDeposit(Guid id)
    {
        return await MakeRequest<DepositResponse>
        (
            () => _apiHelper.DoGet($"/deposits?depositId={id}")
        );
    }

    public async Task<ErrorOr<IEnumerable<DepositResponse>>> GetAllDeposit(string accountIban)
    {
        return await MakeRequest<IEnumerable<DepositResponse>>
        (
            () => _apiHelper.DoGet($"/deposits/all?accountIban={accountIban}")
        );
    }

    public async Task<ErrorOr<SimpleResponse>> CreateDeposit(CreateDepositRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost($"/deposits/create", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<IEnumerable<DepositResponse>>> UpdateDeposit(Guid depositId, UpdateDepositRequest request)
    {
        return await MakeRequest<IEnumerable<DepositResponse>>
        (
            () => _apiHelper.DoPut($"/deposits/update?iban={depositId}", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> DeleteDeposit(Guid depositId)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoDelete($"/deposits/delete?depositIban={depositId}")
        );
    }

}