using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class ImageEditRequest : ImageRequestBase
    {
        
        public byte[]? Image { get; set; }

     
        public string? ImageName { get; set; }

        public byte[]? Mask { get; set; }

        public string? MaskName { get; set; }
        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }
    }
}
