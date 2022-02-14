namespace Kahoot.NET.Internals.Messages.Connect;

/// <summary>
/// A JSON message asking for connection information
/// </summary>
public class LiveConnectionPacket : LiveBaseMessageResponse
{
    /// <summary>
    /// Intializes a new instance of the <see cref="LiveBaseMessageResponse"/>
    /// </summary>
    [JsonConstructor]
    public LiveConnectionPacket()
    {
        Channel = MessageChannels.Connection;
    }
}
