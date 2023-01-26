﻿using Microsoft.Extensions.DependencyInjection;
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

            var version = settings.Value.Version;
            var endPoints = new Endpoints(version);
            var defaultModel = settings.Value.DefaultModel;

            InitializeServices(httpClient, endPoints, defaultModel);
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
            var defaultModel = settings.DefaultModel;

            InitializeServices(httpClient, endPoints, defaultModel);
        }
        private void InitializeServices(HttpClient httpClient, Endpoints endPoints, string? defaultModel)
        {
            CompletionService = new CompletionService(httpClient, endPoints);
            ImageService = new ImageService(httpClient, endPoints);
            ModerationService = new ModerationService(httpClient, endPoints);
            TextEditService = new TextEditService(httpClient, endPoints);
            if (!string.IsNullOrEmpty(defaultModel))
            {
                CompletionService.SetDefaultModel(defaultModel);
            }
        }

        public CompletionService CompletionService { get; private set; } = default!;
        public ImageService ImageService { get; private set; } = default!;
        public ModerationService ModerationService { get; private set; } = default!;
        public TextEditService TextEditService { get; private set; } = default!;
    }
}
