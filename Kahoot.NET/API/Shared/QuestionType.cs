namespace Kahoot.NET.API.Shared;

// see Types.cs Question class

/// <summary>
/// Enum representation of <see cref="Types.Question"/>
/// </summary>
public enum QuestionType
{
    /// <summary>
    /// See <see cref="Types.Question.Quiz"/>
    /// </summary>
    Quiz,

    /// <summary>
    /// See <see cref="Types.Question.MultipleSelect"/>
    /// </summary>
    MultipleSelect,

    /// <summary>
    /// See <see cref="Types.Question.OpenEnded"/>
    /// </summary>
    OpenEnded,

    /// <summary>
    /// See <see cref="Types.Question.WordCloud"/>
    /// </summary>
    WordCloud,

    /// <summary>
    /// See <see cref="Types.Question.Survey"/>
    /// </summary>
    Survey,

    /// <summary>
    /// See <see cref="Types.Question.MultipleSelect"/>
    /// </summary>
    MultipleSelectPoll,

    /// <summary>
    /// See <see cref="Types.Question.Jumble"/>
    /// </summary>
    Jumble,

    /// <summary>
    /// See <see cref="Types.Question.Content"/>
    /// </summary>
    Content,
}
