using System.Net;
using InvBank.Web.Helper.Handlers;

namespace InvBank.Web.Helper;

public class HandlerResponseFactory<T>
{
    public static IHandlerResponse<T> Create(HttpStatusCode statusCode)
    {
        switch (statusCode)
        {
            case HttpStatusCode.OK:
                return new SucessHandler<T>();
            case HttpStatusCode.BadRequest:
                return new BadResponseHandler<T>();
            case HttpStatusCode.Unauthorized:
                return new UnauthorizedHandler<T>();
            default:
                return new DefaultReponseHandler<T>();
        }
    }
}