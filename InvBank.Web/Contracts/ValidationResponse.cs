using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts;

public class ValidationResponse
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("status")]
    public int StatusCode { get; set; }

    [JsonPropertyName("traceId")]
    public string TraceId { get; set; } = null!;

    [JsonPropertyName("errors")]
    public IDictionary<string, IEnumerable<string>> Errors { get; set; } = null!;

}