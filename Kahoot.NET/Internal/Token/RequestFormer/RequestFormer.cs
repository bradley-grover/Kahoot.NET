using Kahoot.NET.Internal.Data.Responses;

namespace Kahoot.NET.Internal.Token.RequestFormer;

internal static class RequestFormer
{
    private const string SessionHeader = "x-kahoot-session-token";
    private const string SessionUrl = "https://kahoot.it/reserve/session/{0}/?{1}";

    internal static async Task<(CreateSessionResponse?, string? header)> CreateSessionAsync(int gameId, HttpClient client)
    {
        var data = await client.SendAsync(NewRequest(gameId));

        if (!data.IsSuccessStatusCode)
        {
            throw new GameNotFoundException();
        }

        var content = await data.Content.ReadAsStringAsync();


        if (!data.Headers.TryGetValues(SessionHeader, out var headers))
        {
            throw new Exception();
        }

        return (JsonSerializer.Deserialize<CreateSessionResponse>(content), headers.FirstOrDefault());
    }

    internal static HttpRequestMessage NewRequest(int gameId)
    {
        HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(string.Format(SessionUrl, gameId, CalculateSeconds()))
        };

        request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
        request.Headers.Add("Referer", "https://kahoot.it");
        request.Headers.Add("Accept", "*/*");

        return request;
    }
    internal static double CalculateSeconds()
    {
        return Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);
    }
}
