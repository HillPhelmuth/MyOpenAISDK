using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Shared;

namespace OpenAIDotNetDemo.Pages
{
    public partial class ChatPage : ComponentBase
    {
        [Inject] private OpenAIDotNetService OpenAiDotNetService { get; set; } = default!;
        private class ChatForm
        {
            public string? Input { get; set; }
            public bool AllowFollowUp { get; set; }
        }
        private ChatForm _chatForm = new();
        private string? _personality;
        private bool _isBusy;
        private List<Message> _messages = new();
        private List<Message> Messages
        {
            get
            {
                if (_messages.Any()) return _messages;
                _messages = new List<Message> { new() { Role = "system", Content = _personality } };
                return _messages;
            }
            set => _messages = value;
        }

        private async void Submit(ChatForm chatForm)
        {
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            var message = new Message { Role = "user", Content = chatForm.Input };
            Messages.Add(message);
            var request = new ChatRequestModel()
            {
                MaxTokens = 256,
                Temperature = 0.9f,
                Messages = Messages
            };
            var response = await OpenAiDotNetService.ChatService.Create(request);
            var reply = response.Choices?.FirstOrDefault()?.Message ?? new Message() { Role = "assistant", Content = $"ERROR: {response.Error?.Message}" };
            Messages.Add(reply);
            _isBusy = false;
            _chatForm = new ChatForm();
            StateHasChanged();
        }

        private void ChangePersona(string persona)
        {
            _personality = persona;
            foreach (var message in Messages.Where(message => message.Role == "system"))
            {
                message.Content = persona;
            }
        }
        private void Reset()
        {
            Messages.Clear();
            _personality = null;
            _chatForm = new ChatForm();
            StateHasChanged();
        }
    }
}
