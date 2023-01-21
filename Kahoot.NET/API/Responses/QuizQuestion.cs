using Kahoot.NET.API.Shared.Extra;

namespace Kahoot.NET.API.Responses;

// the identifier for this response is always null

/// <summary>
/// Represents a quiz question that can be responded to
/// </summary>
public class QuizQuestion : Message<GameContentData>
{
    /// <summary>
    /// The info extracted from the question data
    /// </summary>
    [JsonIgnore]
    public QuizQuestionData? Info { get; set; }

    /// <summary>
    /// The current timetrack
    /// </summary>
    [JsonPropertyName("ext")]
    public ExtWithTimetrack TimetrackExt { get; set; }
}
