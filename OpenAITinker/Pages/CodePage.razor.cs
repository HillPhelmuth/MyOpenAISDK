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
        private List<string> _contentItems = new();
        private bool _isBusy;
        private readonly AceEditorOptions _aceEditorOptions = new()
        {
            Mode = "csharp",
            VScrollBarAlwaysVisible = true,
            Theme = "twilight"

        };

        private string _selectedModel = GptModels.CodeDavinciV2;
        private readonly Dictionary<string, string> _modelOptions = new()
        {
            { "Cushman", GptModels.CodeCushmanV1}, { "Davinci", GptModels.CodeDavinciV2}
        };

        private Dictionary<string, string> _languageModes = new();
        private List<ThemeModel> _themes = new();
        private string _selectedLanguage = "c#";
        
        //protected override Task OnInitializedAsync()
        //{
        //    var files = Directory.EnumerateFiles("_content/BlazorAceEditor/lib/ace", "*.js",
        //        SearchOption.TopDirectoryOnly);
        //    _languageModes = files.Where(x => Path.GetFileNameWithoutExtension(x).StartsWith("mode-")).Select(x => Path.GetFileNameWithoutExtension(x).Replace("mode-", "")).ToList();
        //    _themes = files.Where(x => Path.GetFileNameWithoutExtension(x).StartsWith("theme-")).Select(x => Path.GetFileNameWithoutExtension(x).Replace("theme-", "")).ToList();
        //    return base.OnInitializedAsync();
        //}

        private async Task HandleInit(AceEditor aceEditor)
        {
            await _aceEditor!.SetValue(Snippets.Shuffle);
            
            _languageModes = new Dictionary<string, string>() {{"C++","c_cpp"},{"C#","csharp"}, {"Java","java"} };
            _themes = await AceEditorJsInterop.GetThemes();
            StateHasChanged();
        }

        private async Task HandleLanguageChange(string language)
        {
            await _aceEditor!.ChangeLanguage(language);
        }

        private async Task HandleThemeChange(ThemeModel theme)
        {
            await _aceEditor!.ChangeTheme(theme.Name);
        }
        private async Task Submit()
        {
            //if (string.IsNullOrWhiteSpace(codeText)) return;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            var codeText = await _aceEditor!.GetValue();
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
    }
}
