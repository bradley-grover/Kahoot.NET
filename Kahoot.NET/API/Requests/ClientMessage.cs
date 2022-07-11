namespace Kahoot.NET.API.Requests;

internal class ClientMessage : BaseClientMessage
{
    /// <summary>
    /// Type of connection that the message is using, 
    /// the default is <see cref="InternalConsts.ConnectionType"/>
    /// </summary>
    [JsonPropertyName("connectionType")]
    public string? ConnectionType { get; set; } = Connection.ConnectionType;
}
