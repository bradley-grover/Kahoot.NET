using Kahoot.NET.API.Requests;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async ValueTask SendLeaveMessageAsync()
    {
        await SendAsync(new LeaveMessage()
        {
            Id = Interlocked.Increment(ref State.id).ToString(),
            ClientId = State.clientId,
            Ext = State.OnlyTimeFromState()
        });
    }
}
