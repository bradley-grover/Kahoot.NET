namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ReceiveAsync()
    {
        AssertConnected();

        
#nullable disable
        while (Socket.State == WebSocketState.Open)
        {
#nullable restore
            Memory<byte> buffer = new byte[1024];

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

        var message = JsonSerializer.Deserialize<LiveBaseMessage>(json.AsSpan());

        int id = int.Parse(message!.Id.AsSpan());

        switch ((id, message.Channel))
        {
            case (1, LiveMessageChannels.Handshake):
                var obj = JsonSerializer.Deserialize<LiveClientHandshakeResponse>(json.AsSpan()!);

                if (obj is null)
                {
                    throw new InvalidOperationException("An internal problem occured whilst parsing the websocket data");
                }

                ProcessFirstServerResponse(obj);
                Interlocked.Increment(ref _sessionObject.id);
                await SendAsync(CreateSecondHandshakeObject(obj));
                break;
            default:
                switch (message.Channel)
                {
                    case LiveMessageChannels.Disconnection:
                        break;
                    case LiveMessageChannels.Status:
                        break;
                    case LiveMessageChannels.Player:
                        break;
                    case LiveMessageChannels.Service:
                        break;
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
