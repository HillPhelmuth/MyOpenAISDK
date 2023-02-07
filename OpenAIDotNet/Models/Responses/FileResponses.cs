using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Responses
{
    public class FileListData : BaseResult
    {
        [JsonPropertyName("data")]  
        public List<File>? FileData { get; set; }
    }
    public class FileDeleteResponse : BaseResult
    {
        [JsonPropertyName("deleted")] public bool Deleted { get; set; }
        [JsonPropertyName("id")] public string Id { get; set; }
    }
}
