using Kahoot.NET.API;
using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Hosting.Resolver;

internal static class Session
{
    internal static async Task<(int Code, string Key, bool Success)> SendHostRequestAsync(this HttpClient client, 
        Uri quizUrl, GameConfiguration configuration)
    {
        var response = await client.SendAsync(CreateHostRequest(quizUrl, configuration));

        if (!response.IsSuccessStatusCode || !response.Headers.TryGetValues(Connection.SessionHeader, out var key))
        {
            return (default, default!, false);
        }

        int code = JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync());

        return (code, key!.First(), true);
    }

    internal static HttpRequestMessage CreateHostRequest(Uri uri, GameConfiguration configuration)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(string.Format(Connection.HostSessionUrl, Request.CalculateSeconds()))
        };

        request.Content = new StringContent(JsonSerializer.Serialize(configuration, GameConfigurationContext.Default.GameConfiguration));

        request.Headers.Referrer = uri;

        return request;
    }
}
