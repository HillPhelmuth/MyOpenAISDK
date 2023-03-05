using System.Text.Json.Serialization;

namespace OpenAIDotNet.Models.Shared;

public class Message
{
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    [JsonPropertyName("content")]
    public string? Content { get; set; }
}

public class Delta
{
    [JsonPropertyName("role"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Role { get; set; }

    [JsonPropertyName("content"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Content { get; set; }

    public Message ToMessage()
    {
        return new Message
        {
            Role = string.IsNullOrEmpty(Role) ? "assistant" : Role,
            Content = string.IsNullOrEmpty(Content) ? "" : Content
        };
    }
}