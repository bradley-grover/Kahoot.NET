namespace Kahoot.NET.Exceptions;


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
