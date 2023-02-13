using BlazorAceEditor.Models;
using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Text.Json;
using BlazorAceEditor.Helpers;
using BlazorAceEditor.Models.Events;

namespace BlazorAceEditor
{
    public class AceEditorJsInterop : JSModule
    {
        public event AceChangeEventHandler? AceEditorChange;
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
            var dotNetReference = DotNetObjectReference.Create(this);
            return await InvokeAsync<bool>("init", elementId, optionsDict, dotNetReference);
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

        public async ValueTask<List<ModeModel>> GetLanguageModes()
        {
            var modes = await InvokeAsync<List<ModeModel>>("availableLanguageModes");
            return modes;
        }
        //public async ValueTask Insert(string text)
        //{

        //}
        [JSInvokable]
        public void HandleAceChange(AceChangeEventArgs args)
        {
            AceEditorChange?.Invoke(this, args);
        }
    }
}