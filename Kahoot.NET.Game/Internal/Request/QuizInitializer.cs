using Kahoot.NET.API.Authentication;
using Microsoft.Extensions.Logging;

namespace Kahoot.NET.Game.Internal.Request;

internal static class QuizInitializer
{
    private const string Url = "https://play.kahoot.it/reserve/session/?{0}";
    private static readonly HttpClient _httpClient = new();

    internal static async Task<(int, string)> CreateSessionAsync(string quizUrl, GameConfiguration configuration)
    {
        var response = await _httpClient.SendAsync(CreateRequestMessage(quizUrl, configuration));

        if (!response.Headers.TryGetValues("x-kahoot-session-token", out var webSocketKey))
        {
            throw new Exception();
        }

        return (JsonSerializer.Deserialize<int>(await response.Content.ReadAsStringAsync()), webSocketKey.First());
    }

    internal static HttpRequestMessage CreateRequestMessage(string quizUrl, GameConfiguration configuration)
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(string.Format(Url, API.Authentication.Request.CalculateSeconds())),
        };

        request.Content = new StringContent(JsonSerializer.Serialize(configuration, GameConfigurationContext.Default.GameConfiguration));

        request.Headers.Referrer = new Uri(quizUrl);

        return request;
    }
}
