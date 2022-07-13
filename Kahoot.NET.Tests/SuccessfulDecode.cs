using System.Text.Json.Serialization;

namespace Kahoot.NET.Tests;

#nullable disable

public class SuccessfulDecode
{
    [JsonPropertyName("header")]
    public string Header { get; set; }

    [JsonPropertyName("challenge")]
    public string Challenge { get; set; }

    [JsonPropertyName("expected")]
    public string Expected { get; set; }
}
