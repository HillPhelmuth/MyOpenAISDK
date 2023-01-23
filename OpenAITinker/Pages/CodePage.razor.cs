using System.Text;
using BlazorAceEditor;
using BlazorAceEditor.Models;
using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;

namespace OpenAITinker.Pages
{
    public partial class CodePage
    {
        [Inject] private OpenAIDotNetService OpenAiDotNetService { get; set; } = default!;
        private string? _codeText;
        private string? _responseText;
        private AceEditor? _aceEditor;
        private List<string> _contentItems = new();
        private bool _isBusy;
        private readonly AceEditorOptions _aceEditorOptions = new()
        {
            Mode = "ace/mode/csharp",
            VScrollBarAlwaysVisible = true,
            Theme = "ace/theme/twilight"

        };

        private string _selectedModel = GptModels.CodeDavinciV2;
        private Dictionary<string, string> _modelOptions = new()
        {
            { "Cushman", GptModels.CodeCushmanV1}, { "Davinci", GptModels.CodeDavinciV2}
        };
        private async Task Submit()
        {
            //if (string.IsNullOrWhiteSpace(codeText)) return;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            var codeText = await _aceEditor!.GetValue();
            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine("//c# code");
            promptBuilder.AppendLine(codeText);
            promptBuilder.AppendLine("//Explain the c# code above. Use plain language.");
            promptBuilder.Append("//");

            var prompt = promptBuilder.ToString().Replace("\r\n", "\n");
            var response = await OpenAiDotNetService.CompletionService.Create(new CompletionRequestModel
            {
                Prompt = prompt,
                Model = _selectedModel,
                Temperature = 0,
                N = 1,
                MaxTokens = 256,
                BestOf = 1,
                Stops = new List<string> { "//c# code", "//Explain " },
                TopP = 1,
                FrequencyPenalty = 1.5f,
                Echo = false,
                PresencePenalty = 0,
            });
            var responseText = response.Choices.FirstOrDefault()?.Text ?? "Dick AI didn't provide a response!";
            
            _responseText = responseText;
            var items = _responseText.Split("//");
            _contentItems = items.ToList();
            _isBusy = false;
            StateHasChanged();
        }
    }
}
