using Kahoot.NET.API.Shared.Time;

namespace Kahoot.NET.API.Shared.Extra;

/// <summary>
/// Ext data with timesync data contained with it as well
/// </summary>
/// <typeparam name="T">Should be <see cref="bool"/> or <see cref="long"/></typeparam>
internal class ExtWithTimesync<T> : Ext<T>
{
    /// <summary>
    /// Timesync data to be used with <see cref="Ext{TType}"/>
    /// </summary>
    [JsonPropertyName("timesync")]
    public Timesync? Time { get; set; }
}
