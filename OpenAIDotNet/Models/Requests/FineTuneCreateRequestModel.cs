using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class FineTuneCreateRequestModel
    {
        [JsonPropertyName("training_file")]
        public string? TrainingFile { get; set; }

     
        [JsonPropertyName("validation_file")]
        public string? ValidationFile { get; set; }

      
        [JsonPropertyName("n_epochs")]
        public int? NEpochs { get; set; }

        [JsonPropertyName("batch_size")]
        public int? BatchSize { get; set; }

        
        [JsonPropertyName("learning_rate_multiplier")]
        public float? LearningRateMultiplier { get; set; }

        
        [JsonPropertyName("prompt_loss_weight")]
        public int? PromptLossWeight { get; set; }

    
        [JsonPropertyName("compute_classification_metrics")]
        public bool? ComputeClassificationMetrics { get; set; }

       
        [JsonPropertyName("classification_n_classes")]
        public int? ClassificationNClasses { get; set; }

      
        [JsonPropertyName("classification_positive_class")]
        public string? ClassificationPositiveClass { get; set; }

       
        [JsonPropertyName("classification_betas")]
        public List<object>? ClassificationBetas { get; set; }

       
        [JsonPropertyName("suffix")]
        public string? Suffix { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }
    }
}
