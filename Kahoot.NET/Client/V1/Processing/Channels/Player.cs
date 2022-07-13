namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task PlayerAsync(string jsonContent, string dataType)
    {
        var userObject = JsonSerializer.Deserialize<Message<ContentData>>(jsonContent)!;

        switch (dataType)
        {
            case Types.Message:
                switch (userObject.Data!.Id)
                {
                    case 10:
                        await SendLeaveMessageAsync();
                        await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, default);
                        await Left.InvokeEventAsync(this, new(ReasonForLeaving.UserKicked));

                        break;
                }
                break;
        }
    }
}
