using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class CompletionRequestModel
    {
       
        [JsonIgnore]
        public string? Prompt { get; set; }
       
        [JsonIgnore]
        public IList<string>? PromptList { get; set; }

        [JsonPropertyName("prompt")]
        public IList<string>? Prompts
        {
            get
            {
                if (Prompt != null && PromptList != null)
                {
                    throw new ValidationException("Prompt and PromptList can not be assigned at the same time. One of them is should be null.");
                }
                return Prompt != null ? new List<string>() { Prompt } : PromptList;
            }
        }

        [JsonPropertyName("suffix")]
        public string? Suffix { get; set; }

       
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens { get; set; }

        
        [JsonPropertyName("top_p")]
        public float? TopP { get; set; }

       
        [JsonPropertyName("n")]
        public int? N { get; set; }

       
        [JsonPropertyName("stream")]
        public bool? Stream { get; set; }

        
        [JsonPropertyName("echo")]
        public bool? Echo { get; set; }
        
       

        [JsonPropertyName("stop")]
        public IList<string>? Stops { get; set; }
        

       
        [JsonPropertyName("presence_penalty")]
        public float? PresencePenalty { get; set; }


        
        [JsonPropertyName("frequency_penalty")]
        public float? FrequencyPenalty { get; set; }

       
        [JsonPropertyName("best_of")]
        public int? BestOf { get; set; }

      
        [JsonPropertyName("logit_bias")]
        public object? LogitBias { get; set; }

       
        [JsonPropertyName("logprobs")]
        public int? LogProbs { get; set; }

        [JsonPropertyName("model")] public string? Model { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            throw new NotImplementedException();
        }

        
        [JsonPropertyName("temperature")]
        public float? Temperature { get; set; }

        [JsonPropertyName("user")]
        public string? User { get; set; }
    }
}
