using System.Text.Json;
using Kahoot.NET.API;
using Kahoot.NET.Host.Internals;
using Kahoot.NET.Host.Json;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Host;

public partial class KahootHost
{
    internal async Task SendHandshakeAsync(uint code, string key)
    {
        Uri uri = new(string.Format(ConnectionInfo.HostWebsocketUrl, code, key));

        _logger?.LogDebug("Initializing handshake");

        await _ws.ConnectAsync(uri, default);
    }


    internal static async Task<SessionResult> SendHostRequestAsync(HttpClient client, Uri uri, GameConfiguration config)
    {
        var response = await client.SendAsync(CreateHostRequest(uri, config)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode || !response.Headers.TryGetValues(ConnectionInfo.SessionHeader, out var key))
        {
            return SessionResult.Invalid;
        }

        uint code = JsonSerializer.Deserialize<uint>(await response.Content.ReadAsStringAsync());

        return new SessionResult(code, key!.First());
    }

    internal static HttpRequestMessage CreateHostRequest(Uri uri, GameConfiguration configuration)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(string.Format(ConnectionInfo.HostSessionUrl, DateTimeOffset.UtcNow.ToUnixTimeSeconds())),
            Content = new StringContent(JsonSerializer.Serialize(configuration, GameConfigurationContext.Default.GameConfiguration))
        };

        request.Headers.Referrer = uri;

        return request;
    }
}
