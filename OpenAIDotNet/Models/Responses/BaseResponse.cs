using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Responses
{
    public abstract class BaseResponse
    {
        [JsonPropertyName("object")] public string? ObjectType { get; set; }
        public bool Successful => Error == null;
        [JsonPropertyName("error")] public Error? Error { get; set; }
    }
    public record Error
    {
        [JsonPropertyName("code")] public string? Code { get; set; }

        [JsonPropertyName("message")] public string? Message { get; set; }

        [JsonPropertyName("param")] public string? Param { get; set; }

        [JsonPropertyName("type")] public string? Type { get; set; }
    }
}
