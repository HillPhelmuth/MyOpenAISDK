using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class ImageCreateRequest : ImageRequestBase
    {
        public ImageCreateRequest() { }

        public ImageCreateRequest(string prompt)
        {
            Prompt = prompt;
        }
        [JsonPropertyName("prompt")]
        public string? Prompt { get; set; }
    }
}
