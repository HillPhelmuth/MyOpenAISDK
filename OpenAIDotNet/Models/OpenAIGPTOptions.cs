using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models
{
    public class OpenAIGPTOptions
    {
        public string? Organization { get; set; }
        public string? ApiKey { get; set; } 

        public string Version { get; set; } = "v1";

        public string BaseDomain { get; } = "https://api.openai.com/";
        public string? DefaultModel { get; set; }
      
    }
}
