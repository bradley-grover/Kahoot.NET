namespace Kahoot.NET.Question;

public class QuestionReceivedEventArgs : EventArgs
{
    public IQuestion Question { get; }
    public QuestionReceivedEventArgs(IQuestion question)
    {
        Question = question;
    }
}
