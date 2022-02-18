namespace Kahoot.NET.Exceptions;

/// <summary>
/// Thrown when the challenge provided by Kahoot is unable to be parsed
/// </summary>
[Serializable]
public class InvalidChallengeException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidChallengeException"/> class
    /// </summary>
    public InvalidChallengeException() { }
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidChallengeException"/> class
    /// </summary>
    /// <param name="message">Message of the exception</param>
    public InvalidChallengeException(string message) : base(message) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidChallengeException"/> class
    /// </summary>
    /// <param name="message">Message of the exception</param>
    /// <param name="inner">Inner exception of this execption</param>
    public InvalidChallengeException(string message, Exception inner) : base(message, inner) { }
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidChallengeException"/>
    /// </summary>
    /// <param name="info">Serialization info of the exception</param>
    /// <param name="context">Context of the exception</param>
    protected InvalidChallengeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
