using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;

namespace OpenAIDotNet.Models.Responses
{
    public class ModerationResponseModel : BaseResponse
    {
        [JsonPropertyName("results")] public List<Result>? Results { get; set; }

        [JsonPropertyName("id")] public string? Id { get; set; }

        [JsonPropertyName("model")] public string? Model { get; set; }
    }
    public record Result
    {
        [JsonPropertyName("categories")] public Categories? Categories { get; set; }

        [JsonPropertyName("category_scores")] public CategoryScores? CategoryScores { get; set; }

        [JsonPropertyName("flagged")] public bool Flagged { get; set; }
    }
    public record Categories
    {
        [JsonPropertyName("hate")]
        public bool Hate { get; set; }

        [JsonPropertyName("hate/threatening")]
        public bool HateThreatening { get; set; }

        [JsonPropertyName("self-harm")]
        public bool SelfHarm { get; set; }

        [JsonPropertyName("sexual")]
        public bool Sexual { get; set; }

        [JsonPropertyName("sexual/minors")]
        public bool SexualMinors { get; set; }

        [JsonPropertyName("violence")]
        public bool Violence { get; set; }

        [JsonPropertyName("violence/graphic")]
        public bool ViolenceGraphic { get; set; }
        public Dictionary<string, bool> CategoryValues()
        {
            return this.ObjectPropertyValuesAs<bool>();
        }
    }

    public record CategoryScores
    {
        [JsonPropertyName("hate")]
        public double Hate { get; set; }

        [JsonPropertyName("hate/threatening")]
        public double HateThreatening { get; set; }

        [JsonPropertyName("self-harm")]
        public double SelfHarm { get; set; }

        [JsonPropertyName("sexual")]
        public double Sexual { get; set; }

        [JsonPropertyName("sexual/minors")]
        public double SexualMinors { get; set; }

        [JsonPropertyName("violence")]
        public double Violence { get; set; }

        [JsonPropertyName("violence/graphic")]
        public double ViolenceGraphic { get; set; }
        public Dictionary<string, double> CategoryScoreValues()
        {
            return this.ObjectPropertyValuesAs<double>();
        }
    }
    
}
