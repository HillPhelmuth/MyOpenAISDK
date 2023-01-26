using System.Text.Json.Serialization;

namespace OpenAIDotNet.Models.Responses
{
    public class CompletionResponseModel : BaseResult
    {
        [JsonPropertyName("model")] public string? Model { get; set; }

        [JsonPropertyName("choices")] public List<Choice>? Choices { get; set; }

        [JsonPropertyName("usage")] public TokenUsage? Usage { get; set; }

        [JsonPropertyName("created")] public int CreatedAt { get; set; }

        [JsonPropertyName("id")] public string? Id { get; set; }
    }
    public record Choice
    {
        [JsonPropertyName("text")] public string? Text { get; set; }

        [JsonPropertyName("index")] public int? Index { get; set; }

        [JsonPropertyName("finish_reason")] public string? FinishReason { get; set; }

        [JsonPropertyName("logprobs")] public int? LogProbs { get; set; }
    }
    public record TokenUsage
    {
        [JsonPropertyName("prompt_tokens")] public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int? CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")] public int TotalTokens { get; set; }
    }
}
