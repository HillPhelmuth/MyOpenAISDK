using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class EditRequestModel
    {
        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("input")]
        public string? Input { get; set; }

        [JsonPropertyName("instruction")]
        public string? Instruction { get; set; }
        [JsonPropertyName("top_p")]
        public float? TopP { get; set; }
        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        [JsonPropertyName("n")]
        public int? N { get; set; }
    }
}
