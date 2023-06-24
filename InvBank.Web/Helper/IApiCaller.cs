using System.Net.Http.Headers;

namespace InvBank.Web.Helper;

public interface IApiCaller
{
    Task<HttpResponseMessage> DoGet(string endpoint, IDictionary<string, string>? headers = null);
    Task<HttpResponseMessage> DoPost(string endpoint, string payload, IDictionary<string, string>? headers = null);
    Task<HttpResponseMessage> DoPut(string endpoint, string payload, IDictionary<string, string>? headers = null);
    Task<HttpResponseMessage> DoDelete(string endpoint, IDictionary<string, string>? headers = null);
}