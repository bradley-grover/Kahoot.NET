namespace Kahoot.NET.API.Requests;

/// <summary>
/// Message used when sending a client message, a request
/// </summary>
/// <typeparam name="TData">The type of data that could be contained within the method</typeparam>
public class BaseClientMessage<TData> : Message<TData>
    where TData : Data
{
    /// <summary>
    /// The client identifier which is used to identify which session we are to the server
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; set; }
}

/// <summary>
/// Non generic <see cref="BaseClientMessage{TData}"/> where the data is ignored
/// </summary>
public class BaseClientMessage : BaseClientMessage<Data> { }
