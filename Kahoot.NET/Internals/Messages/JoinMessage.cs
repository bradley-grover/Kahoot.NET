namespace Kahoot.NET.Internals.Messages;

/// <summary>
/// Message to send to connect to the Kahoot
/// </summary>
internal class JoinMessage : IMessageBase
{

    public ReadOnlyMemory<byte> ToRawMessage()
    {
        return Encoding.Unicode.GetBytes(JsonSerializer.Serialize(this));
    }
}
