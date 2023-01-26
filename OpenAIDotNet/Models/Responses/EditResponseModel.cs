using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Responses
{
    public class EditResponseModel : BaseResult
    {
        [JsonPropertyName("model")] public string? Model { get; set; }

        [JsonPropertyName("choices")] public List<Choice>? Choices { get; set; }

        [JsonPropertyName("usage")] public TokenUsage? Usage { get; set; }

        [JsonPropertyName("created")] public int CreatedAt { get; set; }
    }
}
