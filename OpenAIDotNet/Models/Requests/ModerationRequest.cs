using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace OpenAIDotNet.Models.Requests
{
    public class ModerationRequest
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
                    throw new ValidationException("Input and InputList can not both be used. Pick one!");
                }

                if (Input != null)
                {
                    return new List<string>() { Input };
                }

                return InputList;
            }
        }
        [JsonIgnore]
        public ModerationModel ModerationModel { get; set; }
        
        [JsonPropertyName("model")]
        public string? Model
        {
            get
            {
                return ModerationModel switch
                {
                    ModerationModel.Latest => "text-moderation-latest",
                    ModerationModel.Stable => "text-moderation-stable",
                    _ => "text-moderation-latest"
                };
            }
        }
    }

    public enum ModerationModel
    {
        Latest, Stable
    }
}
