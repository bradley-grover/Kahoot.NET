using System.Text.RegularExpressions;
using System.Text;

namespace Kahoot.NET.Internals.Connection;

internal static class Token
{
    /// <summary>
    /// Creates token from Id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    /// <exception cref="GameNotFoundException"></exception>
    public static async Task<string> CreateTokenAsync(int id, HttpClient client)
    {
        var (response, header) = await ConnectionHelper.CreateSessionResponseAsync(id, client);

        if (response is null)
        {
            throw new GameNotFoundException();
        }

        return CombineTokens(header, response.ParseChallenge());
    }
    private static string ParseChallenge(this CreateSessionResponse response) => Decode(response.Challenge!);

    private static string CombineTokens(ReadOnlySpan<char> header, ReadOnlySpan<char> challenge)
    {
        Span<char> stackAllocated = stackalloc char[header.Length];


        for (int i = 0;  i < header.Length; i++)
        {
            char mod = challenge[i % challenge.Length];
            int decoded = header[i] ^ mod;
            stackAllocated[i] = Convert.ToChar(decoded);
        }

        return new string(stackAllocated);
    }

    private static string Decode(ReadOnlySpan<char> challenge)
    {
        int offset = ((41 * 39) * (100 + 11 * 9));

        Span<char> stackAllocatedString = stackalloc char[challenge.Length];
      
        for (int i = 0; i < challenge.Length; i++)
        {
            stackAllocatedString[i] = ChangeChar(challenge[i], i, offset);
        }
        return new string(stackAllocatedString);
    }
    private static char ChangeChar(char value, int position, int offset)
    {
        return Convert.ToChar(((value * position + offset) % 77) + 48);
    }
}
