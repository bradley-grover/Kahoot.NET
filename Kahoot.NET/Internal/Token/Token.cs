using Kahoot.NET.Internal.Token.Processing;

namespace Kahoot.NET.Internal.Token;

internal static class Token
{
    /// <summary>
    /// Creates the token and gets the session data of the game
    /// </summary>
    /// <param name="gameCode">The game id</param>
    /// <param name="client">The <see cref="HttpClient"/> to bs used for internal messaging</param>
    /// <returns>A <see cref="Task{TResult}"/> of the token and the response</returns>
    /// <exception cref="GameNotFoundException">Thrown when the game code is invalid</exception>
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
