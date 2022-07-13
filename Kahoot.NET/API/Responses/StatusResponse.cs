namespace Kahoot.NET.API.Responses;

internal class StatusResponse : Data
{
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
