using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenAIDotNet.Models.Responses;
using OpenAIDotNet.Models.Shared;

namespace OpenAIDotNet.Models.Requests
{
    public class ChatRequestModel : ChatRequestBase
    {
        
        [JsonPropertyName("messages")]
        public List<Message>? Messages { get; set; }

        [JsonPropertyName("model")] 
        public string? Model { get; } = "gpt-3.5-turbo";
    }
}
