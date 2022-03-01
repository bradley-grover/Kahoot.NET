namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ReceiveAsync()
    {
        AssertConnected();

        Memory<byte> buffer = new byte[1024];
#nullable disable
        while (Socket.State == WebSocketState.Open)
        {
#nullable restore
            var result = await Socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }

            await ProcessAsync(buffer);
        }
    }

    private static string RemoveBrackets(ReadOnlySpan<char> span)
    {
        int start = 1;
        int end = span.LastIndexOf(']');

        return span.Slice(start, end - 1).ToString();
    }
    private async Task ProcessAsync(Memory<byte> data)
    {
        string json = RemoveBrackets(Encoding.UTF8.GetString(data.Span));

        Logger?.LogDebug("Response received: {res}", json);

        var message = JsonSerializer.Deserialize<LiveBaseMessage>(json);

        switch ((int.Parse(message!.Id.AsSpan()), message.Channel))
        {
            case (1, LiveMessageChannels.Handshake):
                ProcessFirstServerResponse(JsonSerializer.Deserialize<LiveClientHandshakeResponse>(json.AsSpan())!);
                Interlocked.Increment(ref _sessionObject.id);
                break;

            default:
                switch (message.Channel)
                {
                    case LiveMessageChannels.Handshake:
                        break;
                }
                break;
        }
    }
    private void ProcessFirstServerResponse(LiveClientHandshakeResponse response)
    {
        _sessionObject.clientId = response.ClientId;
    }
}
