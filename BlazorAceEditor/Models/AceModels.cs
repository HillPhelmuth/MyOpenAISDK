using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAceEditor.Models
{
    internal class AceModels
    {
    }
    public record ThemeModel
    {
        [JsonPropertyName("Caption")]
        public string? Caption { get; set; }

        [JsonPropertyName("Theme")]
        public string? Theme { get; set; }

        [JsonPropertyName("IsDark")]
        public bool IsDark { get; set; }

        [JsonPropertyName("Name")]
        public string? Name { get; set; }
    }
}
