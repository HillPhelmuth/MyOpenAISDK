using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;

namespace OpenAIDotNet.Models.Responses
{
    public class FineTuneListResponseModel : BaseResult
    {
        [JsonPropertyName("data")]
        public List<FineTuneListResponseModel>? FineTuneResponseList { get; set; }
    }

    public class FineTuneEventListReponseModel : BaseResult
    {
        [JsonPropertyName("data")]
        public List<FineTuneEvent>? EventList { get; set; }
    }
    public class FineTuneResponseModel : BaseResult
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("events")]
        public List<FineTuneEvent>? Events { get; set; }

        [JsonPropertyName("fine_tuned_model")]
        public string? FineTunedModel { get; set; }

        [JsonPropertyName("hyperparams")]
        public Hyperparams? Hyperparams { get; set; }

        [JsonPropertyName("organization_id")]
        public string? OrganizationId { get; set; }

        [JsonPropertyName("result_files")]
        public List<File>? ResultFiles { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("validation_files")]
        public List<File>? ValidationFiles { get; set; }

        [JsonPropertyName("training_files")]
        public List<File>? TrainingFiles { get; set; }

        [JsonPropertyName("updated_at")]
        public int UpdatedAt { get; set; }
    }

    public class FineTuneEvent
    {
        [JsonPropertyName("object")]
        public string? Object { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("level")]
        public string? Level { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    public class Hyperparams
    {
        [JsonPropertyName("batch_size")]
        public int BatchSize { get; set; }

        [JsonPropertyName("learning_rate_multiplier")]
        public double LearningRateMultiplier { get; set; }

        [JsonPropertyName("n_epochs")]
        public int NEpochs { get; set; }

        [JsonPropertyName("prompt_loss_weight")]
        public double PromptLossWeight { get; set; }
    }

    public class File : BaseResult
    {
        private string? _purpose;

        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("bytes")]
        public int Bytes { get; set; }

        [JsonPropertyName("created_at")]
        public int CreatedAt { get; set; }

        [JsonPropertyName("filename")]
        public string? Filename { get; set; }

        [JsonPropertyName("purpose")]
        public string? Purpose
        {
            get
            {
                if (_purpose is null && FilePurpose is not null)
                    return FilePurpose.Value.ToEnumDescription();
                return _purpose;
            }
            set => _purpose = value;
        }

        [JsonIgnore]
        public FilePurpose? FilePurpose { get; set; }
        
    }

    public enum FilePurpose
    {
        [Description("fine-tune")]
        FineTune,
        [Description("search")]
        Search,
    }
}
