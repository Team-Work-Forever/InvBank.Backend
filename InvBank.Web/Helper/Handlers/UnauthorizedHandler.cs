using ErrorOr;

namespace InvBank.Web.Helper.Handlers;

public class UnauthorizedHandler<T> : IHandlerResponse<T>
{
    public ErrorOr<T> HandleResponse(string response)
    {
        return Error.Unexpected(response);
    }
}