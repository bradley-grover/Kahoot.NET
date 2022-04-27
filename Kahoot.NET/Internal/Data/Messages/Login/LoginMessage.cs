namespace Kahoot.NET.Internal.Data.Messages.Login;

#nullable disable

/// <summary>
/// First login message to send to Kahoot
/// </summary>
internal class LoginMessage : LiveBaseMessage
{
    [JsonPropertyName("data")]
    public LoginData LoginData { get; set; }

    [JsonPropertyName("ext")]
    public object Ext { get; set; }

    [JsonPropertyName("clientId")]
    public string ClientId { get; set; }
}
