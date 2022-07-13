namespace Kahoot.NET.API.Shared;

/// <summary>
/// A message with data embedded in it
/// </summary>
/// <typeparam name="TData">The type of data that the message can hold</typeparam>
internal class Message<TData>
    where TData : Data
{
    /// <summary>
    /// The identifier of the message, incremented after each acknowledgement
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Data that could be possibly included in each message, if it is null it is not included during json serialization
    /// </summary>
    [JsonPropertyName("data")]
    public TData? Data { get; set; }

#nullable disable
    /// <summary>
    /// The channel of the message, this can never be null as it is included in every message, string can be any one of <see cref="Channels"/>
    /// </summary>
    /// <seealso cref="Channels"/>
    [JsonPropertyName("channel")]
    public string Channel { get; set; }
}

/// <summary>
/// Represents a non-generic implementation without any additional data, used as the base to redirect to other processing
/// </summary>
internal class Message : Message<Data> { }
