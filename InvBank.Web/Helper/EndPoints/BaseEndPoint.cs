using ErrorOr;

namespace InvBank.Web.Helper.EndPoints;

public class BaseEndPoint
{
    protected readonly IApiCaller _apiHelper;

    public BaseEndPoint(IApiCaller apiHelper)
    {
        _apiHelper = apiHelper;
    }

    protected delegate Task<HttpResponseMessage> MakeFunc();

    protected async Task<ErrorOr<T>> MakeRequest<T>(MakeFunc apiCall)
    {
        HttpResponseMessage httpResponseMessage = await apiCall.Invoke();
        string result = await httpResponseMessage.Content.ReadAsStringAsync();

        return HandlerResponseFactory<T>.Create(httpResponseMessage.StatusCode).HandleResponse(result);
    }

}