using System.Text.Json;
using ErrorOr;

namespace InvBank.Web.Helper.Handlers;

public class SucessHandler<T> : IHandlerResponse<T>
{
    public ErrorOr<T> HandleResponse(string response)
    {
        T? accountsResult = JsonSerializer.Deserialize<T>(response);

        if (accountsResult is null)
        {
            return Errors.Errors.Auth.RequestFailed;
        }

        return accountsResult;
    }
}