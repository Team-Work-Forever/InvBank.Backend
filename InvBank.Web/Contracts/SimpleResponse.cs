using System.Text.Json.Serialization;

namespace InvBank.Web.Contracts;

public class SimpleResponse
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;
}