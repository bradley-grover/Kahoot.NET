namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task CreateHandshakeAsync(CancellationToken cancellationToken = default)
    {
        if (GameId is null)
        {
            throw new NoGameIdException();
        }

        (var token, var response) = await Token.CreateTokenAndSessionAsync(GameId.Value, Client);

        CreateResponse = response;

        CreateConnectionObject();

        Socket = new();

        Uri uri = new(string.Format(WebsocketUrl, GameId.Value, token));

        Logger?.LogDebug("{url}", uri);

        Logger?.LogDebug("Connecting to socket...");

        await Socket.ConnectAsync(uri, cancellationToken);

        await SendAsync(CreateFirstHandshakeObject(), cancellationToken);
    }



    internal LiveClientHandshake CreateFirstHandshakeObject()
    {
        return new LiveClientHandshake()
        {
            Advice = new() { Interval = 0, Timeout = 60_000 },
            MinimumVersion = InternalConsts.MinVersion,
            Version = InternalConsts.Version,
            SupportedConnectionTypes = InternalConsts.SupportedConnectionTypes,
            Channel = LiveMessageChannels.Handshake,
            Id = Interlocked.Read(ref _sessionObject.id).ToString(),
            Ext = new() { Acknowledged = true, Timesync = new() { L = 0, O = 0 } }
        };
    }

    internal SecondLiveClientHandshake CreateSecondHandshakeObject(LiveClientHandshakeResponse response)
    {
        return new()
        {
            Advice = new() { Interval = 0 },
            ConnectionType = InternalConsts.ConnectionType,
            ClientId = _sessionObject.clientId,
            Ext = new()
            {
                Acknowledged = Interlocked.Read(ref _sessionObject.ack),
                Timesync = new()
                {
                    L = 0,
                    O = 0,
                }
            },
            Id = Interlocked.Read(ref _sessionObject.id).ToString(),
            Channel = LiveMessageChannels.Connection,
        };
    }

    internal void CreateConnectionObject()
    {
        _sessionObject = new()
        {
            id = 1,
            ack = 0,
        };
    }
}
