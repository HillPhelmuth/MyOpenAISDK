using OpenAIDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;
using OpenAIDotNet.Extensions;

namespace OpenAIDotNet.Services
{
    public class ModerationService
    {
        private readonly HttpClient _httpClient;

        public ModerationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ModerationResponseModel> EvaluateContent(ModerationRequest request)
        {
            var url = Endpoints.Moderation;
            return await _httpClient.PostReadJsonAsync<ModerationResponseModel>(url, request);
        }
    }
}
