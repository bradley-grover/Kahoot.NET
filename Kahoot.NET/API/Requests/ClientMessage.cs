namespace Kahoot.NET.API.Requests;

/// <summary>
/// A full client message which contains the connection type as well, not to be used as much as <see cref="BaseClientMessage"/>
/// </summary>
/// <typeparam name="TData">The type of data, if any that it contains</typeparam>
internal class ClientMessage<TData> : BaseClientMessage<TData>
    where TData : Data
{
    /// <summary>
    /// Type of connection that the message is using, 
    /// the default is <see cref="Connection.ConnectionType"/>
    /// </summary>
    [JsonPropertyName("connectionType")]
    public string? ConnectionType { get; set; } = Connection.ConnectionType;
}

/// <summary>
/// Non generic <see cref="ClientMessage{TData}"/> which doesn't contain any data
/// </summary>
internal class ClientMessage : BaseClientMessage<Data> { }
