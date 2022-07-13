namespace Kahoot.NET.API.Shared;

internal class Message<TData>
    where TData : Data
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("data")]
    public TData? Data { get; set; }

#nullable disable
    [JsonPropertyName("channel")]
    public string Channel { get; set; }
}
internal class Message : Message<Data> { }
