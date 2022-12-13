namespace Kahoot.NET.API.Shared.Adv;

/// <summary>
/// Advice used for the server connection timings
/// </summary>
public class Advice : BaseAdvice
{
    /// <summary>
    /// Timeout time for the connection
    /// </summary>
    [JsonPropertyName("timeout")]
    public long Timeout { get; set; }
}
