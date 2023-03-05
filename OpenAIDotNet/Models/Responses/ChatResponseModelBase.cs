using System.Text.Json.Serialization;

namespace OpenAIDotNet.Models.Responses;
[JsonDerivedType(typeof(ChatStreamResponse))]
[JsonDerivedType(typeof(ChatResponseModel))]
public class ChatResponseModelBase : BaseResponse
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("created")]
    public int Created { get; set; }

    [JsonPropertyName("model")]
    public string? Model { get; set; }

    [JsonPropertyName("usage")]
    public TokenUsage? Usage { get; set; }
}