using Kahoot.NET.API.Requests;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task SendPacketAsync()
    {
        await SendAsync(new Packet()
        {
            Id = State.id.ToString(),
            Channel = Channels.Connection,
            Ext = State.ExtWithTimesync,
            ClientId = State.clientId,
            ConnectionType = ConnectionInfo.ConnectionType
        });
    }
}
