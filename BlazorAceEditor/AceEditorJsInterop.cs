using BlazorAceEditor.Models;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Text.Json;
using BlazorAceEditor.Helpers;

namespace BlazorAceEditor
{
    // This class provides an example of how JavaScript functionality can be wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class AceEditorJsInterop : JSModule
    {

        public AceEditorJsInterop(IJSRuntime jsRuntime) 
            : base(jsRuntime, "./_content/BlazorAceEditor/aceEditorInterop.js")
        {
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
            return await InvokeAsync<bool>("init", elementId, optionsDict);
        }

        public async ValueTask<string> GetValue() => await InvokeAsync<string>("getValue");

        public async ValueTask SetValue(string value) => await InvokeVoidAsync("setValue", value);

        public async ValueTask SetLanguage(string language)
        => await InvokeVoidAsync("setLanguage", language);
        public async ValueTask SetTheme(string theme)
        => await InvokeVoidAsync("setTheme", theme);

        public async ValueTask<List<ThemeModel>> GetThemes(bool excludeDark = false)
        {
            return await InvokeAsync<List<ThemeModel>>("availableThemes");
        }
        //public async ValueTask Insert(string text)
        //{

        //}
        
    }
}