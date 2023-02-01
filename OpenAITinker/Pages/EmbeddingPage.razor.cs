using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;
using OpenAIDotNet.Services;
using OpenAITinker.Models;

namespace OpenAITinker.Pages
{
    public partial class EmbeddingPage
    {
        [Inject]
        private OpenAIDotNetService OpenAiDotNetService { get; set; } = default!;
        private string? _input;
        private bool _isBusy;
        private readonly List<StoredEmbedding> _storedEmbeddings = new();
        private StoredEmbedding? _selectedStoredEmbedding;
        private IEnumerable<StoredEmbedding>? _selectedEmbeddings;
        private IEnumerable<SimilarityScore>? _generatedSimilarityScores;
        private async Task Submit()
        {
            if (string.IsNullOrWhiteSpace(_input)) return;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);
            var responseModel = await OpenAiDotNetService.EmbeddingService.Create(new EmbeddingCreateRequest { Input = _input.Replace("\n", " ") });
            _storedEmbeddings.Add(new StoredEmbedding(_input, responseModel.Data.FirstOrDefault().Embedding));
            _isBusy = false;
            _input = null;
            StateHasChanged();

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
