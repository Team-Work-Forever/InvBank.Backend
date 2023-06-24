using System.Text.Json;
using ErrorOr;
using InvBank.Web.Contracts;

namespace InvBank.Web.Helper.Handlers;

public class DefaultReponseHandler<T> : IHandlerResponse<T>
{
    public ErrorOr<T> HandleResponse(string response)
    {
        ErrorResponse? errorResponse = JsonSerializer.Deserialize<ErrorResponse>(response);

        if (errorResponse is null)
        {
            return Error.Unexpected("Ocorreu alguma falha na leitura de dados");
        }

        return Error.Failure(errorResponse.TraceId, errorResponse.Title);
    }
}