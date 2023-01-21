namespace Kahoot.NET.API.Shared;

// see Types.cs Question class

/// <summary>
/// Enum representation of <see cref="Types.Question"/>
/// </summary>
public enum QuestionType
{
    Quiz,
    MultipleSelect,
    OpenEnded,
    WordCloud,
    Survey,
    MultipleSelectPoll,
    Jumble,
    Content,
}
