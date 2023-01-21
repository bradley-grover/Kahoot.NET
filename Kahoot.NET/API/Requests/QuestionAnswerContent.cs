namespace Kahoot.NET.API.Requests;

public class QuestionAnswerContent
{
#nullable disable
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("choice")]
    public object Choice { get; set; }

    [JsonPropertyName("meta")]
    public string Meta { get; set; } = """{ "lag": 30 } """;

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("questionIndex")]
    public int QuestionIndex { get; set; }
}
