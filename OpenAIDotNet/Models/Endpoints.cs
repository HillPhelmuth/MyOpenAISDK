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
        public string CompletionCreate(string engineId)
        {
            return $"/{_apiVersion}/engines/{engineId}/completions";
        }

        public string Completion()
        {
            return $"/{_apiVersion}/completions";
        }
        public string ImageCreate()
        {
            return $"/{_apiVersion}/images/generations";
        }

        public string ImageEditCreate()
        {
            return $"/{_apiVersion}/images/edits";
        }

        public string ImageVariationCreate()
        {
            return $"/{_apiVersion}/images/variations";
        }
    }
}
