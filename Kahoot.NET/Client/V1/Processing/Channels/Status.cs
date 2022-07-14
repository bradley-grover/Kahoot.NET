namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task StatusAsync(string jsonContent, string dataType)
    {
        var statusObject = JsonSerializer.Deserialize<Message<StatusResponse>>(jsonContent)!;

        switch (dataType)
        {
            case Types.Status:
                switch (statusObject.Data!.Status)
                {
                    case Types.Active:
                        await Joined.InvokeEventAsync(this, new() { Success = true });
                        break;
                    case Types.Errors.Locked:
                        await SendLeaveMessageAsync();
                        await Left.InvokeEventAsync(this, new(ReasonForLeaving.GameLocked));
                        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, default);
                        break;
                }
                break;
        }
    }
}
