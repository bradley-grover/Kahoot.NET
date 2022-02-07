namespace Kahoot.NET.Internals.Messages;

/// <summary>
/// Interface to provide a way of sending data to the Kahoot websocket
/// </summary>
public interface IMessageBase
{
    /// <summary>
    /// Returns the message in a data format to send to Kahoot
    /// </summary>
    /// <returns></returns>
    public ReadOnlyMemory<byte> ToRawMessage();
}
