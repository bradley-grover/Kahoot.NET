namespace Kahoot.NET.Internal.Data.Messages.Login;

#nullable disable

internal class UserDeviceModel
{
    [JsonPropertyName("userAgent")]
    public string UserAgent { get; set; }

    [JsonPropertyName("screen")]
    public Screen Screen { get; set; }
}
