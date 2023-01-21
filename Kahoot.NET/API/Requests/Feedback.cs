using Kahoot.NET.API.Json.Converters;

namespace Kahoot.NET.API.Requests;

/// <summary>
/// Feedback to send back to the game after the host has requested it
/// </summary>
public class Feedback
{
#nullable disable
    [JsonPropertyName("nickname")]
    internal string Nickname { get; set; }
#nullable restore

    [JsonPropertyName("totalScore")]
    internal int TotalScore { get; set; }

    /// <summary>
    /// How much fun you had in the game, the max is <see cref="FunMax"/> and the min is <see cref="FunMin"/>
    /// </summary>
    [JsonPropertyName("fun")]
    public uint Fun { get; set; }

    /// <summary>
    /// Whether you learned anything from that Kahoot! game
    /// </summary>
    [JsonPropertyName("learning")]
    [JsonConverter(typeof(NumericBoolConverter))]
    public bool Learning { get; set; }

    /// <summary>
    /// Whether you recommend the finished Kahoot! game
    /// </summary>
    [JsonPropertyName("recommend")]
    [JsonConverter(typeof(NumericBoolConverter))]
    public bool Recommend { get; set; }

    /// <summary>
    /// Valid values are -1, 1
    /// </summary>
    [JsonPropertyName("overall")]
    public int Overall { get; set; }

    /// <summary>
    /// The maximum value allowed for <see cref="Fun"/>
    /// </summary>
    public const uint FunMax = 5;

    /// <summary>
    /// The minimum value allowed for <see cref="Fun"/>
    /// </summary>
    public const uint FunMin = 1;
}
