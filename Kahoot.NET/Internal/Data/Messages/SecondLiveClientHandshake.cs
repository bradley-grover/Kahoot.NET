using Kahoot.NET.Internal.Data.Shared;
using Kahoot.NET.Internal.Data.Shared.Ext;

namespace Kahoot.NET.Internal.Data.Messages;

internal class SecondLiveClientHandshake : ExtendedLiveBaseMessage
{ 
    [JsonPropertyName("advice")]
    public IntervalAdvice? Advice { get; set; }

    [JsonPropertyName("ext")]
    public ExtWithTimesync<long>? Ext { get; set; }
}
