using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class CompletionRequestModel : ChatRequestBase
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
                    throw new ValidationException("Prompt and PromptList can not both be used! Pick one!");
                }

                if (Prompt == null && PromptList == null)
                {
                    throw new ValidationException("You need some kind of prompt. Add a value to either Prompt or PromptList");
                }
                return Prompt != null ? new List<string>() { Prompt } : PromptList;
            }
        }

        [JsonPropertyName("suffix")]
        public string? Suffix { get; set; }
       
        [JsonPropertyName("echo")]
        public bool? Echo { get; set; }

        [JsonPropertyName("best_of")]
        public int? BestOf { get; set; }
      
        [JsonPropertyName("logprobs")]
        public int? LogProbs { get; set; }
        [JsonPropertyName("model")]
        public string? Model { get; set; }

        public IEnumerable<ValidationResult> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
