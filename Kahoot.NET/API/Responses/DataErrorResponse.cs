namespace Kahoot.NET.API.Responses;

internal class DataErrorResponse : Data
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
