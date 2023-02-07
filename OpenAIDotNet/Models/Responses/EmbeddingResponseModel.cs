using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Responses
{
    public class EmbeddingResponseModel : BaseResult
    {

        [JsonPropertyName("data")]
        public List<EmbeddingItem>? Data { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("usage")]
        public Usage? Usage { get; set; }
    }

    public class EmbeddingItem
    {
        [JsonPropertyName("object")]
        public string? Object { get; set; }

        [JsonPropertyName("embedding")]
        public List<double>? Embedding { get; set; }

        [JsonPropertyName("index")]
        public long Index { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public long PromptTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public long TotalTokens { get; set; }
    }

    public record SimilarityScore(string Text1, string Text2, double Cosine);
    
}
