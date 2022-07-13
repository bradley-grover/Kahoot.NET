using Kahoot.NET.API.Shared;

namespace Kahoot.NET.API.Requests;

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
internal class ClientMessage : BaseClientMessage<Data> { }
