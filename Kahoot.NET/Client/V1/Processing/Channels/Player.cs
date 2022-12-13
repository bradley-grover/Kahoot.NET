namespace Kahoot.NET.Client;

public partial class KahootClient
{
    private async Task PlayerAsync(ReadOnlyMemory<byte> jsonContent, string dataType)
    {
        var userObject = JsonSerializer.Deserialize<Message<ContentData>>(jsonContent.Span)!;

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
