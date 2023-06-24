using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;
using InvBank.Web.Contracts.PropertyAccount;

namespace InvBank.Web.Helper.EndPoints;

public class PropertyAccountEndPoint : BaseEndPoint
{
    public PropertyAccountEndPoint(AuthApiHelper apiHelper) : base(apiHelper)
    {
    }

    public async Task<ErrorOr<SimpleResponse>> PayProperty(PayPropertyRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost($"/properties/pay", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<ProfitValueResponse>> GetProfit(Guid id)
    {
        return await MakeRequest<ProfitValueResponse>
        (
            () => _apiHelper.DoGet("/properties/profit?propertyId="+id)
        );
    }

    public async Task<ErrorOr<SimpleResponse>> CreatePropertyAccount(CreatePropertyAccountRequest request)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoPost("/properties/create", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<PropertyAccountResponse>> GetPropertyAccount(Guid id)
    {
        return await MakeRequest<PropertyAccountResponse>
        (
            () => _apiHelper.DoGet($"/properties?propertyAccount={id}")
        );
    }

    public async Task<ErrorOr<IEnumerable<PropertyAccountResponse>>> GetAllPropertyAccount(string accountIban)
    {
        return await MakeRequest<IEnumerable<PropertyAccountResponse>>
        (
            () => _apiHelper.DoGet($"/properties/all?iban={accountIban}")
        );
    }

    public async Task<ErrorOr<IEnumerable<PropertyAccountResponse>>> UpdatePropertyAccount(Guid id, UpdatePropertyAccountRequest request)
    {
        return await MakeRequest<IEnumerable<PropertyAccountResponse>>
        (
            () => _apiHelper.DoPut($"/properties/update?id={id}", JsonSerializer.Serialize(request))
        );
    }

    public async Task<ErrorOr<SimpleResponse>> DeletePropertyAccount(Guid id)
    {
        return await MakeRequest<SimpleResponse>
        (
            () => _apiHelper.DoDelete($"/properties/delete?id={id}")
        );
    }

}