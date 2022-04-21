namespace Kahoot.NET.Internal.Data.Messages.Login;

internal class Screen
{
    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}
