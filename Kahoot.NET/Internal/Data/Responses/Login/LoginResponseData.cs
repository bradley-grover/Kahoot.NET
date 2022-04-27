namespace Kahoot.NET.Internal.Data.Responses.Login;

internal struct LoginResponseData
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("cid")]
    public string? CId { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
