using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public abstract class ChatRequestBase
    {
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }

        [JsonPropertyName("top_p")]
        public float? TopP { get; set; }

        [JsonPropertyName("n")]
        public int? N { get; set; }

        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }
        [JsonPropertyName("stop")]
        public IList<string>? Stops { get; set; }

        [JsonPropertyName("presence_penalty")]
        public float? PresencePenalty { get; set; }

        [JsonPropertyName("frequency_penalty")]
        public float? FrequencyPenalty { get; set; }
        [JsonPropertyName("logit_bias")]
        public object? LogitBias { get; set; }
        

        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        [JsonPropertyName("user")]
        public string? User { get; set; }
    }
}
