using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;
using OpenAIDotNet.Services;

namespace OpenAIDotNetDemo.Pages
{
    public partial class EmbeddingPage
    {
        [Inject]
        private OpenAIDotNetService OpenAiDotNetService { get; set; } = default!;
        private string? _input;
        private readonly List<InputItem> _inputs = new(){new InputItem {Id = 0, Value = ""}};
        private bool _isBusy;
        private readonly List<StoredEmbedding> _storedEmbeddings = new();
        private StoredEmbedding? _selectedStoredEmbedding;
        private IEnumerable<StoredEmbedding>? _selectedEmbeddings;
        private IEnumerable<SimilarityScore>? _generatedSimilarityScores;

        private class InputItem
        {
            public int Id { get; set; }
            public string Value { get; set; } = "";
        }
        private async Task Submit()
        {
            if (_inputs.Count <= 1) return;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            _storedEmbeddings.Clear();
            foreach (var input in _inputs)
            {
                var responseModel = await OpenAiDotNetService.EmbeddingService.Create(new EmbeddingCreateRequest { Input = input.Value.Replace("\r\n", " ").Replace("\n", " ") });
                _storedEmbeddings.Add(new StoredEmbedding(input.Value, responseModel.Data.FirstOrDefault().Embedding));
            }
            
            _isBusy = false;
            _input = null;
            StateHasChanged();

        }

        private void AddInput()
        {
             var index = _inputs.Count;
            _inputs.Add(new InputItem {Id = index});
        }

        private void RemoveInput(int index)
        {
            var input = _inputs.Find(x => x.Id == index);
            if (input == null || input.Id == 0) return;
            _inputs.Remove(input);
        }
        private void HandleSelected(StoredEmbedding stored)
        {
            _selectedStoredEmbedding = stored;
            _storedEmbeddings.Remove(stored);
            StateHasChanged();
        }

        private void HandleTabChange(int index)
        {
            if (index > 0 || _selectedStoredEmbedding is null) return;
            _storedEmbeddings.Add(_selectedStoredEmbedding);
            _selectedStoredEmbedding = null;
        }
        private void GetSimilarities()
        {
            if (_selectedEmbeddings == null || _selectedEmbeddings.Count() < 2) return;
            _generatedSimilarityScores = EmbeddingService.GetAllSimilaryScores(_selectedEmbeddings).OrderByDescending(x => x.Cosine);
        }

        private void GetOneToManySimilarity()
        {
            if (_selectedStoredEmbedding is null || _selectedEmbeddings is null) return;
            _generatedSimilarityScores = EmbeddingService.GetSimilarityScores(_selectedStoredEmbedding, _selectedEmbeddings).OrderByDescending(x => x.Cosine);
        }


    }
}
