namespace Kahoot.NET.API.Requests.Login;

/// <summary>
/// The login message to send to the server
/// </summary>
internal class LoginMessage : BaseClientMessage<LoginInformation>
{
    /// <summary>
    /// Intializes a new instance of the <see cref="LoginMessage"/> class
    /// </summary>
    public LoginMessage()
    {
        Channel = Channels.Service;
        Ext = new { };
    }

    /// <summary>
    /// Empty object data to send
    /// </summary>
    [JsonPropertyName("ext")]
    public object Ext { get; set; }
}
