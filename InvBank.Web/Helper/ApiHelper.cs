using System.Net.Http.Headers;
using System.Text;

namespace InvBank.Web.Helper;

public class ApiHelper : IApiCaller
{
    private readonly string apiUrl = "https://localhost:7022";
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public ApiHelper(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    protected HttpRequestMessage GenericApiCall(string endpoint, HttpMethod method, IDictionary<string, string>? headers = null)
    {
        var url = apiUrl + endpoint;
        var request = new HttpRequestMessage(method, url);

        if (headers is null)
        {
            return request;
        }

        foreach (var header in headers)
        {
            if (header.Key.Equals("Authorization"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", header.Value);
                continue;
            }

            request.Headers.Add(header.Key, header.Value);
        }

        return request;

    }

    protected HttpRequestMessage GenericApiCall(string endpoint, HttpMethod method, string payload, IDictionary<string, string>? headers = null)
    {
        var request = GenericApiCall(endpoint, method, headers);
        request.Content = new StringContent(payload, Encoding.UTF8, "application/json");

        return request;
    }

    public virtual async Task<HttpResponseMessage> DoDelete(string endpoint, IDictionary<string, string>? headers = null)
    {
        var request = GenericApiCall(endpoint, HttpMethod.Delete, headers);
        return await _httpClient.SendAsync(request);
    }

    public virtual async Task<HttpResponseMessage> DoGet(string endpoint, IDictionary<string, string>? headers = null)
    {
        var request = GenericApiCall(endpoint, HttpMethod.Get, headers);
        return await _httpClient.SendAsync(request);
    }

    public virtual async Task<HttpResponseMessage> DoPost(string endpoint, string payload, IDictionary<string, string>? headers = null)
    {
        var request = GenericApiCall(endpoint, HttpMethod.Post, payload, headers);
        return await _httpClient.SendAsync(request);
    }

    public virtual async Task<HttpResponseMessage> DoPut(string endpoint, string payload, IDictionary<string, string>? headers = null)
    {
        var request = GenericApiCall(endpoint, HttpMethod.Put, payload, headers);
        return await _httpClient.SendAsync(request);
    }
}