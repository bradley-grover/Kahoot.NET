namespace Kahoot.NET.API.Shared.Extra;

/// <summary>
/// Ext data for the client
/// </summary>
/// <typeparam name="TType">Should be <see cref="bool"/> and <see cref="long"/></typeparam>
public class Ext<TType>
{
    /// <summary>
    /// If the connection was acknowledged, this should be used with <see cref="long"/> or <see cref="bool"/>
    /// </summary>
    [JsonPropertyName("ack")]
    public TType? Acknowledged { get; set; }
}
