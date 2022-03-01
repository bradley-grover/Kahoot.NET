using Kahoot.NET.Internal.Token.Processing;

namespace Kahoot.NET.Internal.Token;

internal static class Token
{
    internal static async Task<(string, CreateSessionResponse)> CreateTokenAndSessionAsync(int gameCode, HttpClient client)
    {
        var (response, header) = await RequestFormer.RequestFormer.CreateSessionAsync(gameCode, client);

        if (response is null)
        {
            throw new GameNotFoundException();
        }

        return (Merger.Create(Header.CreateHeaderToken(header), Challenge.CreateToken(response.Challenge!)), response);
    }
}
