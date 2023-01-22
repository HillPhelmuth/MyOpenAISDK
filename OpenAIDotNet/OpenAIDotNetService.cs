using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenAIDotNet.Models;
using OpenAIDotNet.Services;

namespace OpenAIDotNet
{
    public class OpenAIDotNetService
    {
        [ActivatorUtilitiesConstructor]
        public OpenAIDotNetService(HttpClient httpClient, IOptions<OpenAIGPTOptions> settings)
        {
            Console.WriteLine("DI Container used 1st ctor");
            httpClient.BaseAddress = new Uri(settings.Value.BaseDomain);
            var authKey = settings.Value.ApiKey;
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authKey}");
            var organization = settings.Value.Organization;
            if (!string.IsNullOrEmpty(organization))
            {
                httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", $"{organization}");
            }

            var endPoints = new Endpoints(settings.Value.Version);
            CompletionService = new CompletionService(httpClient, endPoints);
            ImageService = new ImageService(httpClient, endPoints);
            if (!string.IsNullOrEmpty(settings.Value.DefaultModel))
            {
                CompletionService.SetDefaultModel(settings.Value.DefaultModel);
            }
        }
        public OpenAIDotNetService(HttpClient httpClient, OpenAIGPTOptions settings)
        {
            Console.WriteLine("DI Container used 2nd ctor");
            httpClient.BaseAddress = new Uri(settings.BaseDomain);
            var authKey = settings.ApiKey;
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {authKey}");
            var organization = settings.Organization;
            if (!string.IsNullOrEmpty(organization))
            {
                httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", $"{organization}");
            }

            var endPoints = new Endpoints(settings.Version);
            CompletionService = new CompletionService(httpClient, endPoints);
            ImageService = new ImageService(httpClient, endPoints);
            if (!string.IsNullOrEmpty(settings.DefaultModel))
            {
                CompletionService.SetDefaultModel(settings.DefaultModel);
            }
        }

        public CompletionService CompletionService { get; }
        public ImageService ImageService { get; }

    }
}
