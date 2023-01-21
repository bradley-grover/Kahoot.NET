using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Responses;

// the identifier for this response is always null

/// <summary>
/// Represents a quiz question that can be responded to
/// </summary>
public class QuizQuestion : Message<GameContentData>
{
    [JsonIgnore]
    public QuizQuestionData? Info { get; set; }

    [JsonPropertyName("ext")]
    public ExtWithTimetrack TimetrackExt { get; set; }
}
