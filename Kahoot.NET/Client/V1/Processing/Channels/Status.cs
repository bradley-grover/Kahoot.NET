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
                        Debug.WriteLine("[CLIENT]: ACTIVE SIGNAL RECEIVED");
                        await Joined.InvokeEventAsync(this, new() { Success = true });
                        break;
                    case Types.Errors.Locked:
                        Debug.WriteLine("[CLIENT]: LOCKED SIGNAL RECEIVED");
                        await SendLeaveMessageAsync();
                        await Left.InvokeEventAsync(this, new(ReasonForLeaving.GameLocked));
                        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, default);
                        break;
                    case Types.Errors.Queue:
                        Debug.WriteLine("[CLIENT]: QUEUE SIGNAL RECEIVED");
                        await SendLeaveMessageAsync();
                        await Left.InvokeEventAsync(this, new(ReasonForLeaving.QueueFull));
                        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, default);
                        break;
                }
                break;
        }
    }
}
