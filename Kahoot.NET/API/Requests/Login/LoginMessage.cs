namespace Kahoot.NET.API.Requests.Login;

internal class LoginMessage : BaseClientMessage<LoginInformation>
{
    public LoginMessage()
    {
        Channel = Channels.Service;
        Ext = new { };
    }

    [JsonPropertyName("ext")]
    public object Ext { get; set; }
}
