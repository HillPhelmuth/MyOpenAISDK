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

        public async Task<string?> PromptTextResponse(string prompt, string model = "", string user = "")
        {
            var requestModel = new CompletionRequestModel
            {
                Prompt = prompt,
                Model = string.IsNullOrEmpty(model) ? _defaultModel : model,
                User = user,
                N = 1
            };
            var response = await Create(requestModel);
            return response.Successful
                ? response.Choices.FirstOrDefault()?.Text
                : $"PromptTextResponse request failed.\n{response.Error?.Message}";
        }
        public IAsyncEnumerable<CompletionResponseModel> CreateStream(CompletionRequestModel request)
        {
            request.Model ??= _defaultModel;
            request.Stream ??= true;
            var url = _endPoints.Completion();
            return _httpClient.PostReadJsonStream<CompletionResponseModel>(url, request);
        }

        public async IAsyncEnumerable<string> PromptTextResponseStream(string prompt, string model = "", string user = "")
        {
            var requestModel = new CompletionRequestModel
            {
                Prompt = prompt,
                Model = string.IsNullOrEmpty(model) ? _defaultModel : model,
                User = user,
                N = 1,
                Stream = true
            };
            var responseStream = CreateStream(requestModel);
            await foreach (var response in responseStream)
            {
               yield return (response?.Successful == true
                   ? response.Choices.FirstOrDefault()?.Text
                   : $"PromptTextResponse request failed.\n{response?.Error?.Message}") ?? string.Empty;

            }
        }
        internal void SetDefaultModel(string model)
        {
            _defaultModel = model;
        }
    }
}
