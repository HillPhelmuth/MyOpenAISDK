using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAceEditor.Models
{
    public class AceSessionOptions
    {
        [JsonPropertyName("firstLineNumber")]
        public int? FirstLineNumber { get; set; }

        [JsonPropertyName("overwrite")]
        public bool? Overwrite { get; set; }

        [JsonPropertyName("newLineMode")] 
        public string? NewLineMode => Enum.GetName(NewLineModeOption)?.ToLower();
        [JsonIgnore]
        public NewLineModeOption NewLineModeOption { get; set; }

        [JsonPropertyName("useWorker")]
        public bool? UseWorker { get; set; }

        [JsonPropertyName("useSoftTabs")]
        public bool? UseSoftTabs { get; set; }

        [JsonPropertyName("tabSize")]
        public int? TabSize { get; set; }

        [JsonPropertyName("wrap")]
        public int? Wrap { get; set; }

        [JsonPropertyName("foldStyle")]
        public string? FoldStyle => Enum.GetName(FoldStyleOption)?.ToLower();
        [JsonIgnore]
        public FoldStyleOption FoldStyleOption { get; set; }

        [JsonPropertyName("mode")]
        public string? Mode { get; set; }
    }

    public enum NewLineModeOption
    {
        Auto, Unix, Window
    }

    public enum FoldStyleOption
    {
        MarkBegin, MarkBeginEnd, Manual
    }
}
