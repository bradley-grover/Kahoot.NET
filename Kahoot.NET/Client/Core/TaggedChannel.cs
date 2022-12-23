using Kahoot.NET.API.Requests.Handshake;
using Kahoot.NET.API.Requests.Login;

namespace Kahoot.NET.Client;

// this file contains the code to handle a tagged channel message, eg we should handle it in a fast path case, so most of these are for joining the game initially as the
// the number Id should be the same 

public partial class KahootClient
{
    internal async Task ProcessTaggedChannelAsync(ReadOnlyMemory<byte> data, uint id, string channel, string? dataType = null)
    {
        switch ((id, channel))
        {
            case (1, Channels.Handshake):
                var handshakeResponse = JsonSerializer.Deserialize(data.Span, BaseCMessageContext.Default.BaseClientMessage);

                if (handshakeResponse is null) return; // TODO: Add better support for error correction

                _stateObject.clientId = handshakeResponse.ClientId;

                Interlocked.Increment(ref _stateObject.id);
                await SendSecondHandshakeAsync();

                break;
            case (2, Channels.Connect):
                Bump();

                await SendFinalHandshakeAsync();
                await Task.Delay(800); // TODO: Investigate which delay can allow for clients to still join
                await SendLoginMessageAsync();

                break;
            case (4, Channels.Service):
            case (_, Channels.Connect):
                await ReplyAsync();
                break;
            default:
                await ProcessChannelAsync(data, channel, dataType);
                break;
        }
    }

    internal async Task SendLoginMessageAsync()
    {
        Debug.Assert(_username != null);

        await SendAsync(new LoginMessage()
        {
            Id = Interlocked.Increment(ref _stateObject.id).ToString(),
            ClientId = _stateObject.clientId,
            Data = new LoginInformation(
                _username,
                _code.ToString(),
                JsonSerializer.Serialize(new
                {
                    device = new Device() { Screen = Screen.Default, UserAgent = _userAgent }
                }))
        });
    }

    internal ValueTask SendSecondHandshakeAsync() => SendAsync(new SecondClientHandshake()
    {
        Id = Interlocked.Read(ref _stateObject.id).ToString(),
        ClientId = _stateObject.clientId,
        Ext = new()
        {
            Acknowledged = (long)Interlocked.Read(ref _stateObject.ack),
            Time = new()
            {
                L = _stateObject.l,
                O = _stateObject.o,
            }
        }
    }, SecondHandshakeContext.Default.SecondClientHandshake);

    internal ValueTask SendFinalHandshakeAsync() => SendAsync(new FinalHandshake()
    {
        ClientId = _stateObject.clientId,
        Id = Interlocked.Read(ref _stateObject.id).ToString(),
        Ext = new()
        {
            Acknowledged = (long)Interlocked.Read(ref _stateObject.ack),
            Time = new()
            {
                L = _stateObject.l,
                O = _stateObject.o,
            }
        }

    }, FinalHandshakeContext.Default.FinalHandshake);
}
