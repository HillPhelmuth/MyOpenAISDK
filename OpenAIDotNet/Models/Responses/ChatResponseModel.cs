using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenAIDotNet.Models.Shared;

namespace OpenAIDotNet.Models.Responses
{
    public class ChatResponseModel : BaseResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("created")]
        public int Created { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("usage")]
        public TokenUsage? Usage { get; set; }

        [JsonPropertyName("choices")]
        public List<ChatChoice>? Choices { get; set; }
    }

    public class ChatChoice
    {
        [JsonPropertyName("message")]
        public Message? Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string? FinishReason { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }
    }
}
