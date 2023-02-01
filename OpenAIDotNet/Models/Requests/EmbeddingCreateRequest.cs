using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class EmbeddingCreateRequest
    {
        [JsonIgnore]
        public List<string>? InputList { get; set; }

        [JsonIgnore]
        public string? Input { get; set; }
        
        [JsonPropertyName("input")]
        public IList<string>? Inputs
        {
            get
            {
                if (Input != null && InputList != null)
                {
                    throw new ValidationException("Input and InputList can not both be used! Pick one!");
                }

                if (Input == null && InputList == null)
                {
                    throw new ValidationException("You need some kind of input. Add a value to either Input or InputList");
                }
                return Input != null ? new List<string>() { Input } : InputList;
            }
        }


        [JsonPropertyName("model")] 
        public string? Model { get; set; } = GptModels.EmbedAdaV2;
        [JsonPropertyName("user")]
        public string? User { get; set; }
    }
    public record StoredEmbedding(string Text, List<double> Embeddings);
}
