using Kahoot.NET.API.Shared;

namespace Kahoot.NET.API.Responses;

internal class DataErrorResponse : Data
{
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
