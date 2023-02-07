using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIDotNet.Services
{
    public class FineTuningService
    {
        private readonly HttpClient _httpClient;

        public FineTuningService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
    }
}
