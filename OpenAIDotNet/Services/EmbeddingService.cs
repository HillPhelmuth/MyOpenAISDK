using OpenAIDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNet.Services
{
    public class EmbeddingService
    {
        private readonly HttpClient _httpClient;

        public EmbeddingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmbeddingResponseModel> Create(EmbeddingCreateRequest request)
        {
            var url = Endpoints.Embedding;
            return await _httpClient.PostReadJsonAsync<EmbeddingResponseModel>(url, request);
        }

        public async Task<IEnumerable<SimilarityScore>> CreateAndCompareTo(EmbeddingCreateRequest request,
            IEnumerable<StoredEmbedding> storedEmbeddings)
        {
            var response = await Create(request);
            var storedList = new List<StoredEmbedding>();
            var input = string.Join(" ", request.Inputs);
            var embeddings = response.Data.FirstOrDefault().Embedding;
            storedList.Add(new StoredEmbedding(input, embeddings));
            storedList.AddRange(storedEmbeddings);
            return GetAllSimilaryScores(storedList);
        }

        public async Task<IEnumerable<SimilarityScore>> CreateAndCompareTo(EmbeddingCreateRequest request, string text,
            IReadOnlyList<double> embeddings)
        {
            var storedList = new List<StoredEmbedding> { new(text, embeddings.ToList()) };
            return await CreateAndCompareTo(request, storedList);
        }
        private static double GetCosineSimilarity(IReadOnlyList<double> v1, IReadOnlyList<double> v2)
        {
            var n = v2.Count < v1.Count ? v2.Count : v1.Count;
            var dot = 0.0d;
            var mag1 = 0.0d;
            var mag2 = 0.0d;
            for (var i = 0; i < n; i++)
            {
                dot += v1[i] * v2[i];
                mag1 += Math.Pow(v1[i], 2);
                mag2 += Math.Pow(v2[i], 2);
            }
           
            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
        public static IEnumerable<SimilarityScore> GetAllSimilaryScores(IEnumerable<StoredEmbedding> embeddings)
        {
            var embeddingArray = embeddings.ToArray();
            for (var i = 0; i < embeddingArray.Length - 1; i++)
            {
                for (var j = i + 1; j < embeddingArray.Length; j++)
                {
                    var text1 = embeddingArray[i].Text;
                    var text2 = embeddingArray[j].Text;
                    var embeds1 = embeddingArray[i].Embeddings;
                    var embeds2 = embeddingArray[j].Embeddings;
                    var cosine = GetCosineSimilarity(embeds1, embeds2);
                    
                    yield return new SimilarityScore(text1, text2, cosine);
                }
            }
        }

        public static IEnumerable<SimilarityScore> GetSimilarityScores(StoredEmbedding compare,
            IEnumerable<StoredEmbedding> compareTo)
        {
            var text = compare.Text;
            var embeds = compare.Embeddings;
            foreach (var (text2, embeddings) in compareTo)
            {
                yield return new SimilarityScore(text, text2, GetCosineSimilarity(embeds, embeddings));
            }
        }
    }
}
