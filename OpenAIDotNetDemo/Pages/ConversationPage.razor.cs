using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNetDemo.Pages
{
    public partial class ConversationPage
    {
        [Inject] private OpenAIDotNetService OpenAIDotNetService { get; set; } = default!;
        
        private readonly CompletionRequestModel _completionRequest = new(){MaxTokens = 256, BestOf = 1, Stream = false, Prompt = InitialPrompt};
        private CompletionResponseModel? _completionResponse;
        private bool _isBusy;
        private Dictionary<string, string> _modelOptions => new()
        {
            {"Davinci", GptModels.TextDavinciV3}, {"Curie", GptModels.TextCurieV1},
            {"Babbage", GptModels.TextBabbageV1}, {"Ada", GptModels.TextAdaV1}
        };

        private CompletionRequestForm _completionRequestForm = new();
        private class CompletionRequestForm
        {
            public string? Prompt { get; set; } = InitialPrompt;
            public float? PresencePenalty { get; set; }
            public float? FrequencyPenalty { get; set; }
            public string? Model { get; set; } = GptModels.TextDavinciV3;
            public float? Temperature { get; set; }
            public string StopInput { get; set; } = "Human: ,AI: ";
            public int Number { get; set; } = 1;
            public int? BestOf { get; set; }
        }

        private async Task HandleSubmit(CompletionRequestForm completionRequestForm)
        {
            _completionRequest.Model = completionRequestForm.Model;
            _completionRequest.PresencePenalty = completionRequestForm.PresencePenalty;
            _completionRequest.FrequencyPenalty = completionRequestForm.FrequencyPenalty;
            _completionRequest.Prompt = completionRequestForm.Prompt;
            _completionRequest.Temperature = completionRequestForm.Temperature;
            _completionRequest.Stops = completionRequestForm.StopInput.Split(',').ToList();
            _completionRequest.N = completionRequestForm.Number;
            _completionRequest.BestOf = completionRequestForm.BestOf;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            
            _completionResponse = await OpenAIDotNetService.CompletionService.Create(_completionRequest);
            _isBusy = false;
            StateHasChanged();
        }

        private const string InitialPrompt = @"The following is a conversation with an AI assistant. The assistant is helpful, creative, clever, and very friendly.

Human: Hello, who are you?
AI: I am an AI created by OpenAI. How can I help you today?
Human: Where do you live?
AI:";
    }
}
