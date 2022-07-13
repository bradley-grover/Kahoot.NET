namespace Kahoot.NET.API.Requests.Login;

internal class FinalLoginMessage : BaseClientMessage<FinalLoginInformation>
{
    public FinalLoginMessage()
    {
        Channel = Channels.Service;
        Ext = new { };
    }

    [JsonPropertyName("ext")]
    public object Ext { get; set; }
}
