namespace Kahoot.NET.Exceptions;


[Serializable]
public class NoGameIdException : Exception
{
    public NoGameIdException() { }
    public NoGameIdException(string message) : base(message) { }
    public NoGameIdException(string message, Exception inner) : base(message, inner) { }
    protected NoGameIdException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
