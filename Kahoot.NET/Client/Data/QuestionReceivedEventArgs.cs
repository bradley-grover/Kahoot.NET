namespace Kahoot.NET.Client.Data;

/// <summary>
/// Event data for when the client receives a question
/// </summary>
public class QuestionReceivedEventArgs : EventArgs
{
    /// <summary>
    /// The question that the client has received
    /// </summary>
    public QuestionReceivedData Question { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="QuestionReceivedEventArgs"/> class with the specified <see cref="QuestionReceivedData"/>
    /// </summary>
    public QuestionReceivedEventArgs(QuestionReceivedData question)
    {
        ArgumentNullException.ThrowIfNull(question);

        Question = question;
    }
}
