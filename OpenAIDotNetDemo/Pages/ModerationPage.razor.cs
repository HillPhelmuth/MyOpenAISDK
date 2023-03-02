using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNetDemo.Pages
{
    public partial class ModerationPage
    {
        [Inject] private OpenAIDotNetService OpenAIDotNetService { get; set; } = default!;
        private IEnumerable<ModerationModel> ModerationModels => Enum.GetValues<ModerationModel>().Cast<ModerationModel>().ToList();
        private readonly ModerationRequestForm _moderationRequestForm = new();
        private bool _isBusy;
        private ModerationResponseModel? _responseModel;
        private readonly Dictionary<string, ModerationResultScore> _resultScores = new();
        private class ModerationResultScore
        {
            public ModerationResultScore(bool isFlagged, double score)
            {
                IsFlagged = isFlagged;
                Score = score;
            }
            public bool IsFlagged { get; }
            public double Score { get; }
        }
        private class ModerationRequestForm
        {
            public string? Input { get; set; }
            public ModerationModel? SelectedModel { get; set; }
        }

        private async void HandleSubmit(ModerationRequestForm form)
        {
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            var modResult = await OpenAIDotNetService.ModerationService.EvaluateContent(new ModerationRequest
            {
                Input = form.Input,
                ModerationModel = form.SelectedModel ?? ModerationModel.Latest,
            });
            _responseModel = modResult;
            var topResult = _responseModel.Results?.FirstOrDefault();
            var categoryValues = topResult?.Categories?.CategoryValues();
            var categoryScores = topResult?.CategoryScores?.CategoryScoreValues();
            var keyList = categoryValues?.Keys.ToList();
            foreach (var key in keyList ?? new List<string>())
            {
                var score = categoryScores?.TryGetValue(key, out var value) == true ? value : 0;
                _resultScores[key] = new ModerationResultScore(categoryValues?[key] ?? false, score);
            }
            _isBusy = false;
            StateHasChanged();
        }
    }
}
