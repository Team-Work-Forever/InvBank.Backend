using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.Authentication;

namespace InvBank.Web.Helper.EndPoints;

public class AuthenticationEndPoint : BaseEndPoint
{
    public AuthenticationEndPoint(ApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<AuthenticationResult>> Login(LoginRequest request)
    {
        return await MakeRequest<AuthenticationResult>(
            () => _apiHelper.DoPost("/auth/login", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<AuthenticationResult>> RegisterClient(RegisterClientRequest request)
    {
        return await MakeRequest<AuthenticationResult>(
            () => _apiHelper.DoPost("/auth/register/client", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> RegisterCompany(RegisterCompanyRequest request)
    {
        return await MakeRequest<SimpleResponse>(
            () => _apiHelper.DoPost("/auth/register/company", JsonSerializer.Serialize(request))
        );
    }

}