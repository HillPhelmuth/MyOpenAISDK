using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenAITinker.Models
{
    public class DemoContent
    {
        [JsonPropertyName("Topic")]
        public string? Topic { get; set; }

        [JsonPropertyName("Description")]
        public string? Description { get; set; }

        [JsonPropertyName("Short")]
        public string? Short { get; set; }
        public static List<DemoContent>? FromJson(string json) => JsonSerializer.Deserialize<List<DemoContent>>(json);
        
    }
}
