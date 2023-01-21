using Kahoot.NET.API.Requests.Handshake;

namespace Kahoot.NET.Client;

public partial class KahootClient
{
    internal async Task<bool> TryConnectAsync(CancellationToken cancellationToken = default)
    {
        var session = await Session.CreateAsync(_httpClient, _code);

        if (!session.Success)
        {
            return false;
        }

        _usingNamerator = session.Namerator;

        string gameUrl = UriHelper.CreateGameUrl(_code, session.WebSocketKey);

        Uri uri = new(gameUrl);

        _logger?.LogDebug("{websocketUrl}", gameUrl);

        try
        {
            await _ws.ConnectAsync(uri, cancellationToken).ConfigureAwait(false);
        }
        catch (WebSocketException ex)
        {
            _logger?.LogDebug("[KAHOOT.NET]:\nCould not connect, status code: {code}", Enum.GetName(ex.WebSocketErrorCode));
            return false;
        }

        await SendAsync(new ClientHandshake()
        {
            Id = Interlocked.Read(ref _stateObject.id).ToString(),
            Ext = new()
            {
                Acknowledged = true,
                Time = new() { L = 0, O = 0 }
            }
        }, ClientHandshakeContext.Default.ClientHandshake, cancellationToken);

        return true;
    }
}
