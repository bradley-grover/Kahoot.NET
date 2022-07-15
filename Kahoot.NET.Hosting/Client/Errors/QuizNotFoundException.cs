namespace Kahoot.NET.Hosting.Client.Errors;

/// <summary>
/// Exception thrown when a quiz the client is trying to host is not found
/// </summary>
[Serializable]
public class QuizNotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuizNotFoundException"/> class with the specified message
    /// </summary>
    /// <param name="message">Exception message for debugging</param>
    public QuizNotFoundException(string message) : base(message) { }
}
