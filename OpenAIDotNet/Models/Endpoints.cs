using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models
{
    public class Endpoints
    {
        private readonly string _apiVersion;

        public Endpoints(string apiVersion)
        {
            _apiVersion = apiVersion;
        }
        
        public string Completion => $"/{_apiVersion}/completions";
        public string ImageCreate => $"/{_apiVersion}/images/generations";

        public string ImageEditCreate => $"/{_apiVersion}/images/edits";

        public string ImageVariationCreate => $"/{_apiVersion}/images/variations";

        public string Moderation => $"/{_apiVersion}/moderations";

        public string TextEdit => $"/{_apiVersion}/edits";
    }
}
