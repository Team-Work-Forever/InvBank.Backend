using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;

namespace InvBank.Web.Helper.Handlers;

public class BadResponseHandler<T> : IHandlerResponse<T>
{
    public ErrorOr.ErrorOr<T> HandleResponse(string response)
    {
        ValidationResponse? errorResponse = JsonSerializer.Deserialize<ValidationResponse>(response);

        if (errorResponse is null)
        {
            return Error.Unexpected("Ocorreu alguma falha na leitura de dados");
        }

        return Error.Validation(errorResponse.TraceId, errorResponse.Title);
    }
}