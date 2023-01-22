using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenAIDotNet.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<TResponse> PostReadJsonAsync<TResponse>(this HttpClient client, string uri, object requestModel)
        {
            var response = await client.PostAsJsonAsync(uri, requestModel, new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            });
            return await response.Content.ReadFromJsonAsync<TResponse>() ?? throw new InvalidOperationException();
        }
        public static async Task<TResponse> PostFileReadJsonAsync<TResponse>(this HttpClient client, string uri, HttpContent content)
        {
            var response = await client.PostAsync(uri, content);
            return await response.Content.ReadFromJsonAsync<TResponse>() ?? throw new InvalidOperationException();
        }
        public static async IAsyncEnumerable<TResponse> PostReadJsonStream<TResponse>(this HttpClient httpClient, string? path, object requestModel)
        {
            using HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, path);
            req.Content = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(responseStream);
                string? line = null;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.StartsWith("data: "))
                        line = line[$"data: ".Length..];

                    if (string.IsNullOrWhiteSpace(line) || line == "[DONE]") continue;
                    var t = JsonSerializer.Deserialize<TResponse>(line.Trim());
                    yield return t;
                }
            }
        }
    }
}
