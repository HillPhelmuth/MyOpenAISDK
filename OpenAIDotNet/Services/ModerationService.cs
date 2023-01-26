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

        private readonly Endpoints _endPoints;

        public ModerationService(HttpClient httpClient, Endpoints endPoints)
        {
            _httpClient = httpClient;
            _endPoints = endPoints;
        }

        public async Task<ModerationResponseModel> EvaluateContent(ModerationRequest request)
        {
            var url = _endPoints.Moderation;
            return await _httpClient.PostReadJsonAsync<ModerationResponseModel>(url, request);
        }
    }
}
