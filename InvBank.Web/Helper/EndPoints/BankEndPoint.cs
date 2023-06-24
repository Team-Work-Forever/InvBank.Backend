using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.Bank;

namespace InvBank.Web.Helper.EndPoints;

public class BankEndPoint : BaseEndPoint
{
    public BankEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<IEnumerable<BankResponse>>> GetAllBanks()
    {
        return await MakeRequest<IEnumerable<BankResponse>>
        (
            () => _apiHelper.DoGet("/banks")
        );
    }

    public async Task<ErrorOr<SimpleResponse>> CreateBank(RegisterBankRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost("/banks/create", JsonSerializer.Serialize(request))
        );
    }

}