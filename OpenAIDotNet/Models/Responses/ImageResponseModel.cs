using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Responses
{
    public class ImageResponseModel : BaseResult
    {
        [JsonPropertyName("data")] public List<ImageData> Results { get; set; }

        [JsonPropertyName("created")] public int CreatedAt { get; set; }

        public record ImageData
        {
            [JsonPropertyName("url")] public string? Url { get; set; }
            [JsonPropertyName("b64_json")] public string? B64 { get; set; }
        }
    }
}
