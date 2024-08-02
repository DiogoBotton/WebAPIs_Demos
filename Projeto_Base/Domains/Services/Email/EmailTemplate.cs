using System.Text.Json.Serialization;

namespace Domains.Services.Email;

public class EmailTemplate
{
    [JsonPropertyName("sender")]
    public SenderSend Sender { get; set; }

    [JsonPropertyName("messageVersions")]
    public List<MessageVersion> MessageVersions { get; set; } = new();

    [JsonPropertyName("subject")]
    public string Subject { get; set; }

    [JsonPropertyName("templateId")]
    public int TemplateId { get; set; }

    [JsonPropertyName("params")]
    public dynamic Params { get; set; }
}

public class SenderSend
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }
}

public class MessageVersion
{
    [JsonPropertyName("to")]
    public List<ToSend> To { get; set; }
}

public class ToSend
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
}
