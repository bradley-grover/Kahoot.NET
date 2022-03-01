namespace Kahoot.NET.Internal.Data.Shared.Ext;

internal class ExtWithTimesync<T> : Ext<T>
{
    [JsonPropertyName("timesync")]
    public Timesync.TimesyncData? Timesync { get; set; }
}
