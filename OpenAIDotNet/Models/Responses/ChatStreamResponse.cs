using System.Text.Json.Serialization;
using OpenAIDotNet.Models.Shared;

namespace OpenAIDotNet.Models.Responses
{
    public class ChatStreamResponse : ChatResponseModelBase
    {
        [JsonPropertyName("choices")]
        public List<StreamChoice>? Choices { get; set; }
    }

    public class StreamChoice
    {
        [JsonPropertyName("delta")]
        public Delta? Delta { get; set; }
        [JsonPropertyName("finish_reason")]
        public string? FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
