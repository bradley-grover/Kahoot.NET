namespace Kahoot.NET.API.Responses;

/// <summary>
/// Represents a error contained within a <see cref="Data"/>
/// </summary>
public class DataErrorResponse : Data
{
    /// <summary>
    /// The type of error that occured
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
