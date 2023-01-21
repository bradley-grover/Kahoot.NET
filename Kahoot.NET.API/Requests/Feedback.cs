using Kahoot.NET.API.Json.Converters;

namespace Kahoot.NET.API.Requests;

/// <summary>
/// Data to send for when the host requests feedback from the players
/// </summary>
public class Feedback
{
#nullable disable
    [JsonPropertyName("nickname")]
    internal string Nickname { get; set; }
#nullable restore

    [JsonPropertyName("totalScore")]
    internal int TotalScore { get; set; }

    [JsonPropertyName("fun")]
    public uint Fun { get; set; }

    [JsonPropertyName("learning")]
    [JsonConverter(typeof(NumericBoolConverter))]
    public bool Learning { get; set; }

    [JsonPropertyName("recommend")]
    [JsonConverter(typeof(NumericBoolConverter))]
    public bool Recommend { get; set; }

    [JsonPropertyName("overall")]
    public int Overall { get; set; }

    public const uint FunMax = 5;
    public const uint FunMin = 1;
}
