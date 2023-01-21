namespace Kahoot.NET.Client.Events;

#nullable disable

/// <summary>
/// Represents a question received by the client
/// </summary>
public class QuestionReceivedArgs : EventArgs
{
    /// <summary>
    /// The quiz data received for the question
    /// </summary>
    public QuizQuestionData Question { get; set; }

    /// <summary>
    /// You should ignore the question because there is no way to respond to it
    /// </summary>
    public bool ShouldIgnore => Types.Question.GetQuestionType(Question?.Type) == QuestionType.Content;  
}
