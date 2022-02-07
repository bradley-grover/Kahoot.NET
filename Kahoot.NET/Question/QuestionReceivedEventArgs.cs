namespace Kahoot.NET.Question;

/// <summary>
/// Event data object for when the client receives a question
/// </summary>
public class QuestionReceivedEventArgs : EventArgs
{
    /// <summary>
    /// Question that was received
    /// </summary>
    public IQuestion Question { get; }
    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionReceivedEventArgs"/> class
    /// </summary>
    /// <param name="question"></param>
    public QuestionReceivedEventArgs(IQuestion question)
    {
        Question = question;
    }
}
