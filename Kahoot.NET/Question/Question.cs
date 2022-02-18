namespace Kahoot.NET.Question;

/// <summary>
/// Represents a question in Kahoot
/// </summary>
public class Question : IQuestion
{
    /// <inheritdoc></inheritdoc>
    public async Task AnswerAsync(int id)
    {
    }
    /// <inheritdoc></inheritdoc>
    public int Position { get; }
    /// <inheritdoc></inheritdoc>
    public TimeSpan TimeLeft { get; }
    /// <inheritdoc></inheritdoc>
    public QuestionType QuestionType { get; }
    /// <inheritdoc></inheritdoc>
    public bool Ended { get; }
    /// <inheritdoc></inheritdoc>
    public int Number { get; }
    /// <inheritdoc></inheritdoc>
    public GameMode GameMode { get; }
}
