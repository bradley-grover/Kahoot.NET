namespace Kahoot.NET;

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
}
