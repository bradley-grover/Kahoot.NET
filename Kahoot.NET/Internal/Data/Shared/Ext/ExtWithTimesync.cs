namespace Kahoot.NET.Internal.Data.Shared.Ext;

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
    public Timesync.TimesyncData? Timesync { get; set; }
}
