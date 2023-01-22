using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OpenAIDotNet.Models;

namespace OpenAIDotNet.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenAIDotNet(this IServiceCollection services, Action<OpenAIGPTOptions> optionsAction)
        {
            services.AddOptions<OpenAIGPTOptions>().Configure(optionsAction);
            services.AddHttpClient<OpenAIDotNetService>();
            return services.AddScoped<OpenAIDotNetService>();
        }

        public static IServiceCollection AddOpenAIDotNet(this IServiceCollection services, string apiKey,
            string? organization = null, string? defaultModel = null)
        {
            services.AddOptions<OpenAIGPTOptions>();
            services.AddHttpClient<OpenAIDotNetService>();
            var options = new OpenAIGPTOptions
            { ApiKey = apiKey, Organization = organization, DefaultModel = defaultModel };
            services.Configure(SetOptions(options));
            return services.AddScoped<OpenAIDotNetService>();
        }

        private static Action<OpenAIGPTOptions> SetOptions(OpenAIGPTOptions options)
        {
            return aigptOptions =>
            {
                aigptOptions.ApiKey = options.ApiKey;
                aigptOptions.Organization = options.Organization;
                aigptOptions.DefaultModel = options.DefaultModel;
            };
        }

    }
}
