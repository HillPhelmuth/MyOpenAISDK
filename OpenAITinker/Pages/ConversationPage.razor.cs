using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAITinker.Pages
{
    public partial class ConversationPage
    {
        [Inject] private OpenAIDotNetService OpenAIDotNetService { get; set; } = default!;
        
        private string input;
        private CompletionRequestModel _completionRequest = new(){MaxTokens = 256, BestOf = 1, Stream = false, Prompt = InitialPrompt};
        private CompletionResponseModel? _completionResponse;
        private bool _isBusy;
        private string _stopInput = "Human: ,AI: ";
        private Dictionary<string, string> _modelOptions => new()
        {
            {"Davinci", GptModels.TextDavinciV3}, {"Curie", GptModels.TextCurieV1},
            {"Babbage", GptModels.TextBabbageV1}, {"Ada", GptModels.TextAdaV1}
        };
        

        private async Task HandleSubmit(CompletionRequestModel completionRequest)
        {
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            completionRequest.Stops = _stopInput.Split(',').ToList();
            _completionResponse = await OpenAIDotNetService.CompletionService.Create(completionRequest);
            _isBusy = false;
            StateHasChanged();
        }

        private const string InitialPrompt = @"The following is a conversation with an AI assistant. The assistant is helpful, creative, clever, and very friendly.

Human: Hello, who are you?
AI: I am an AI created by OpenAI. How can I help you today?
Human: Where do you live?
AI:";
    }
    public record ConvoBit(string User, string Response);
}
