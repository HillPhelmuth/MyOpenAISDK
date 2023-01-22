using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public abstract class ImageRequestBase
    {

        [JsonPropertyName("n")]
        public int? N { get; set; }


        [JsonPropertyName("size")]
        public string? Size
        {
            get
            {
                return ImageSize switch
                {
                    ImageSize.Size512 => "512x512",
                    ImageSize.Size256 => "256x256",
                    ImageSize.Size1024 => "1024x1024",
                    _ => null
                };
            }
        }
        [JsonIgnore]
        public ImageSize ImageSize { get; set; }

        [JsonPropertyName("response_format")]
        public string? ResponseFormat
        {
            get
            {
                return ImageResponseFormat switch
                {
                    ImageFormat.Base64 => "b64_json",
                    ImageFormat.Url => "url",
                    _ => null
                };
            }
        }
        [JsonIgnore]
        public ImageFormat? ImageResponseFormat { get; set; }


        [JsonPropertyName("user")]
        public string? User { get; set; }
    }
    public enum ImageSize
    {
        Size256, Size512, Size1024
    }

    public enum ImageFormat
    {
        Url, Base64
    }
}
