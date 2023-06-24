using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.Account;

namespace InvBank.Web.Helper.EndPoints;

public class AccountEndPoint : BaseEndPoint
{
    public AccountEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<SimpleResponse>> MakeTransfer(string accountIban, MakeTransferRequest request)
    {
        return await MakeRequest<SimpleResponse>(() => _apiHelper.DoPost($"/accounts/make/transfer?accountIban={accountIban}", JsonSerializer.Serialize(request)));
    }

    public async Task<ErrorOr<AccountResponse>> CreateAccount(CreateAccountRequest request)
    {
        return await MakeRequest<AccountResponse>(() => _apiHelper.DoPost($"/accounts/create", JsonSerializer.Serialize(request)));
    }

    public async Task<ErrorOr<AccountResponse>> GetAccount(string iban)
    {
        return await MakeRequest<AccountResponse>(() => _apiHelper.DoGet($"/accounts?iban={iban}"));
    }

    public async Task<ErrorOr<IEnumerable<AccountResponse>>> GetAllAccount()
    {
        return await MakeRequest<IEnumerable<AccountResponse>>(() => _apiHelper.DoGet($"/accounts/all"));
    }

    public async Task<ErrorOr<SimpleResponse>> DeleteAccount(string iban)
    {
        return await MakeRequest<SimpleResponse>(
            () => _apiHelper.DoDelete($"/accounts/delete?iban={iban}"));
    }

}