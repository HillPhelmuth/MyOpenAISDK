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
        [Inject] private AceEditorJsInterop AceEditorJsInterop { get; set; } = default!;
        private string? _codeText;
        private string? _responseText;
        private AceEditor? _aceEditor;
        private AceEditor? _aceEditor2;
        private List<string> _contentItems = new();
        private bool _isBusy;
        private readonly AceEditorOptions _aceEditorOptions = new()
        {
            Mode = "csharp",
            VScrollBarAlwaysVisible = true,
            Theme = "gob"

        };

        private string _selectedModel = GptModels.CodeDavinciV2;
        private readonly Dictionary<string, string> _modelOptions = new()
        {
            { "Cushman", GptModels.CodeCushmanV1}, { "Davinci", GptModels.CodeDavinciV2}
        };

        private Dictionary<string, string> _languageModes = new();
        private List<ThemeModel> _themes = new();
        private string _selectedLanguage = "c#";

        private int _tabIndex;
        
        private async Task HandleInit(AceEditor aceEditor)
        {
            var code = _tabIndex switch
            {
                0 => Snippets.Shuffle,
                1 => Snippets.RequestGenerate
            };
            await _aceEditor!.SetValue(code);
            
            _languageModes = new Dictionary<string, string>() {{"C++","c_cpp"},{"C#","csharp"}, {"Java","java"} };
            _themes = await AceEditorJsInterop.GetThemes();
            StateHasChanged();
        }

        private async Task HandleLanguageChange(string language, AceEditor editor)
        {
            await editor!.ChangeLanguage(language);
        }

        private async Task HandleThemeChange(ThemeModel theme, AceEditor editor)
        {
            await editor!.ChangeTheme(theme.Name);
        }

        private async Task Submit()
        {
            var codeTask = _tabIndex switch
            {
                0 => Submit(_aceEditor),
                1 => SubmitGenerate(_aceEditor2),
            };
            await codeTask;
        }
        private async Task Submit(AceEditor editor)
        {
            //if (string.IsNullOrWhiteSpace(codeText)) return;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            var codeText = await editor!.GetValue();
            var promptBuilder = new StringBuilder();
            promptBuilder.AppendLine($"//{_selectedLanguage} code");
            promptBuilder.AppendLine(codeText);
            promptBuilder.AppendLine($"//Explain the {_selectedLanguage} code above. Use plain language.");
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
                FrequencyPenalty = 0.5f,
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

        public async Task SubmitGenerate(AceEditor editor)
        {
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);

            var codeText = await editor!.GetValue();
            var request = new CompletionRequestModel
            {
                Prompt = $"{codeText.Replace("\r\n", "\n")}\n",
                Model = _selectedModel,
                Temperature = 0,
                N = 1,
                MaxTokens = 400,
                BestOf = 1,
                Stops = new List<string> {"/*" },
                TopP = 1,
                Echo = false,
                PresencePenalty = 1.0f,
            };
            var response = await OpenAiDotNetService.CompletionService.Create(request);
            var responseText = response.Choices.FirstOrDefault()?.Text ?? "Dick AI didn't provide a response!";
            var editorText = $"{codeText}\n\n{responseText}";
            await editor!.SetValue(editorText);
            _isBusy = false;
            StateHasChanged();
        }
    }

    public static class Snippets
    {
        public const string Shuffle = @"public static void Shuffle<T>(this IList<T> cards)
{
	var rng = new Random();
	int n = cards.Count;
	while (n > 1)
	{
		n--;
		int k = rng.Next(n + 1);
		T value = cards[k];
		cards[k] = cards[n];
		cards[n] = value;
	}
}";

        public const string RequestGenerate = @"/* C# Code Generate a generic method to shuffle cards. 
It should take a type paramater T and collection of type T parameter named cards */";
    }
}
