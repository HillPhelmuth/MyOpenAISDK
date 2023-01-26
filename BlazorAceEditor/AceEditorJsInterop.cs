using BlazorAceEditor.Models;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BlazorAceEditor
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class AceEditorJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public AceEditorJsInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/BlazorAceEditor/aceEditorInterop.js").AsTask());
        }

        public async ValueTask<bool> Init(string elementId, AceEditorOptions options)
        {
            //convert to JsonElement to remove all null properties from object
            var optionsJson = JsonSerializer.Serialize(options, new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            Console.WriteLine($"Options as Json:\n{optionsJson}");
            var optionsDict = JsonSerializer.Deserialize<JsonElement>(optionsJson);
            var module = await moduleTask.Value;
            return await module.InvokeAsync<bool>("init", elementId, optionsDict);
        }

        public async ValueTask<string> GetValue()
        {
            return await (await moduleTask.Value).InvokeAsync<string>("getValue");
        }

        public async ValueTask SetValue(string value)
        {
            await (await moduleTask.Value).InvokeVoidAsync("setValue", value);
        }
        public async ValueTask SetLanguage(string language)
        {
            await (await moduleTask.Value).InvokeVoidAsync("setLanguage", language);
        }
        public async ValueTask SetTheme(string theme)
        {
            await (await moduleTask.Value).InvokeVoidAsync("setTheme", theme);
        }

        public async ValueTask<List<ThemeModel>> GetThemes(bool excludeDark = false)
        {
            return await (await moduleTask.Value).InvokeAsync<List<ThemeModel>>("availableThemes");
        }
        //public async ValueTask Insert(string text)
        //{

        //}
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}