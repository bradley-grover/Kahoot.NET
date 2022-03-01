namespace Kahoot.NET.Internal.Data.Shared.Ext;

internal class Ext<TType>
{
    [JsonPropertyName("ack")]
    public TType? Acknowledged { get; set; }
}
