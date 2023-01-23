using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAceEditor.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorAceEditor(this IServiceCollection services)
        {
            return services.AddScoped<AceEditorJsInterop>();
        }
    }
}
