using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;
using OpenAIDotNet.Models;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNet.Services
{
    public class CompletionService
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _endPoints;
        private string _defaultModel = GptModels.TextAdaV1;

        public CompletionService(HttpClient httpClient, Endpoints endPoints)
        {
            _httpClient = httpClient;
            _endPoints = endPoints;
        }

        public async Task<CompletionResponseModel> Create(CompletionRequestModel request)
        {
            request.Model ??= _defaultModel;
            var url = _endPoints.Completion();
            return await _httpClient.PostReadJsonAsync<CompletionResponseModel>(url, request);
        }

        public IAsyncEnumerable<CompletionResponseModel> CreateStream(CompletionRequestModel request)
        {
            request.Model ??= _defaultModel;
            var url = _endPoints.Completion();
            return _httpClient.PostReadJsonStream<CompletionResponseModel>(url, request);
        }
        internal void SetDefaultModel(string model)
        {
            _defaultModel = model;
        }
    }
}
