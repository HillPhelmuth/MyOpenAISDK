using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;
using OpenAIDotNet.Models;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNet.Services
{
    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = Endpoints.Chat;

        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ChatResponseModel> Create(ChatRequestModel request)
        {
            return await _httpClient.PostReadJsonAsync<ChatResponseModel>(_url, request);
        }

        public async IAsyncEnumerable<ChatStreamResponse> CreateStream(ChatRequestModel request)
        {
            if (request.Stream != true)
                throw new ValidationException(
                    $"CreateStream requires a request with {nameof(ChatRequestModel.Stream)} set to 'true' ");
            
            var response = _httpClient.PostReadJsonStream<ChatStreamResponse>(_url, request);
            await foreach (var chunk in response)
            {
                yield return chunk;
            }
        }

        public async IAsyncEnumerable<ChatStreamResponse> CreateChatStream(ChatRequestModel request)
        {
            using var httpResponseMessage = await _httpClient.PostAsStream(_url, request);
            await using var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            using var streamReader = new StreamReader(stream);
            while (streamReader.EndOfStream is false)
            {
                var line = await streamReader.ReadLineAsync();
                if (string.IsNullOrEmpty(line)) continue;
                var dataPosition = line.IndexOf("data: ", StringComparison.Ordinal);
                line = dataPosition != 0 ? line : line["data: ".Length..];
                Console.WriteLine($"Line data:\n{line}\n");
                if (line.StartsWith("[DONE]")) break;
                ChatStreamResponse? response;
                try
                {
                    response = JsonSerializer.Deserialize<ChatStreamResponse>(line);
                }
                catch(Exception ex)
                {
                    line += await streamReader.ReadToEndAsync();
                    response = JsonSerializer.Deserialize<ChatStreamResponse>(line);
                }

                if (response != null) 
                    yield return response;

            }
        }
    }
}
