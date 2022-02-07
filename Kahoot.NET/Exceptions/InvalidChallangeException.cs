namespace Kahoot.NET.Exceptions;

/// <summary>
/// Thrown when the challenge provided by Kahoot is unable to be parsed
/// </summary>

[Serializable]
public class InvalidChallengeException : Exception
{
    public InvalidChallengeException() { }
    public InvalidChallengeException(string message) : base(message) { }
    public InvalidChallengeException(string message, Exception inner) : base(message, inner) { }
    protected InvalidChallengeException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
