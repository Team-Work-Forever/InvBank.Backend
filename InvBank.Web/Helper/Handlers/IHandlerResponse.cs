using ErrorOr;

namespace InvBank.Web.Helper.Handlers;

public interface IHandlerResponse<T>
{
    ErrorOr<T> HandleResponse(string response);
}