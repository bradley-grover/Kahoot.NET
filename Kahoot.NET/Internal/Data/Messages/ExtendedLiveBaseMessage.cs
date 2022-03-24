namespace Kahoot.NET.Internal.Data.Messages;

/// <summary>
/// Extended version of <see cref="LiveBaseMessage"/> for after the first messages are sent
/// </summary>
internal class ExtendedLiveBaseMessage : LiveBaseMessage
{
    /// <summary>
    /// The sessions client id, in this case our id is used so that the server remembers us
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }

    /// <summary>
    /// Type of connection that the message is using, 
    /// the default is <see cref="InternalConsts.ConnectionType"/>
    /// </summary>
    [JsonPropertyName("connectionType")]
    public string? ConnectionType { get; set; }
}
