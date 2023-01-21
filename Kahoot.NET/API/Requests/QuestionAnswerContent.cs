namespace Kahoot.NET.API.Requests;

/// <summary>
/// The content used to answer the question
/// </summary>
public class QuestionAnswerContent
{
#nullable disable
    /// <summary>
    /// The question answer choice used this can be 4 types, number, array, string or null
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("choice")]
    public object Choice { get; set; }

    /// <summary>
    /// The meta data used by the game
    /// </summary>
    [JsonPropertyName("meta")]
    public string Meta { get; set; } = """{ "lag": 30 } """;

    /// <summary>
    /// The type of question being responded to
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// The question index in the game
    /// </summary>
    [JsonPropertyName("questionIndex")]
    public int QuestionIndex { get; set; }
}
