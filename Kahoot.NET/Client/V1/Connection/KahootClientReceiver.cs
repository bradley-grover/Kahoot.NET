using System.Buffers;
using Kahoot.NET.API;
using Kahoot.NET.Internal.Data.Responses.Handshake;
using Kahoot.NET.Internal.Data.Responses.Login;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task ReceiveAsync()
    {
        if (Socket.State is not WebSocketState.Open)
        {
            if (OnClientError is not null)
            {
                await OnClientError.Invoke(this, new(new InvalidOperationException("Connection is not open for operation")));
            }
        }

        while (Socket.State == WebSocketState.Open)
        {
            byte[] array = ArrayPool<byte>.Shared.Rent(StateObject.BufferSize);
                
            Memory<byte> buffer = array;

            var result = await Socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await Socket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }

            await ProcessAsync(buffer);

            ArrayPool<byte>.Shared.Return(array, true);
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

        var message = JsonSerializer.Deserialize(json.AsSpan(), LiveBaseMessageContext.Default.LiveBaseMessage);

        int id = message!.Id is null ? -1 : int.Parse(message!.Id.AsSpan());

        switch ((id, message.Channel))
        {
            case (1, LiveMessageChannels.Handshake):
                var obj = JsonSerializer.Deserialize(json.AsSpan()!, LiveClientHandshakeResponseContext.Default.LiveClientHandshakeResponse);

                if (obj is null)
                {
                    throw new InvalidOperationException("An internal problem occured whilst parsing the websocket data");
                }

                ProcessFirstServerResponse(obj);
                Interlocked.Increment(ref State.id);
                await SendAsync(CreateSecondHandshakeObject(obj));
                break;
            case (2, LiveMessageChannels.Connection):
                Interlocked.Increment(ref State.id);
                Interlocked.Increment(ref State.ack);

                await SendAsync(CreateFinalHandshake(), FinalHandshakeContext.Default.FinalHandshake);
                await Task.Delay(800);
                await SendLoginAsync();

                break;
            default:
                switch (message.Channel)
                {
                    case LiveMessageChannels.Connection:
                        await SendKeepAliveAsync();
                        break;
                    case LiveMessageChannels.Disconnection:
                        break;
                    case LiveMessageChannels.Status:
                        break;
                    case LiveMessageChannels.Player:
                        break;
                    case LiveMessageChannels.Service:
                        var loginResponse = JsonSerializer.Deserialize<LoginResponse>(json.AsSpan()!);
                        await HandleLoginResponseAsync(loginResponse!);

                        break;
                    case LiveMessageChannels.Handshake:
                        break;
                }
                break;
        }
    }

    private void ProcessFirstServerResponse(LiveClientHandshakeResponse response)
    {
        State.clientId = response.ClientId;
    }
}
