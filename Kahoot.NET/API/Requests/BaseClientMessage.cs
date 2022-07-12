using Kahoot.NET.API.Shared;

namespace Kahoot.NET.API.Requests;

internal class BaseClientMessage<TData> : Message<TData>
    where TData : Data
{
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}
internal class BaseClientMessage : BaseClientMessage<Data> { }
