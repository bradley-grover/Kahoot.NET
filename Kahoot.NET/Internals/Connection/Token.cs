using System.Text.RegularExpressions;
using System.Text;

namespace Kahoot.NET.Internals.Connection;

internal static class Token
{
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
        StringBuilder builder = new(header.Length);

        for (int i = 0;  i < header.Length; i++)
        {
            char c = header[i];
            char mod = challenge[i % challenge.Length];
            int decoded = c ^ mod;
            builder.Append(Convert.ToChar(decoded));
        }
        
        return builder.ToString();
    }

    private static string Decode(ReadOnlySpan<char> challenge)
    {
        int offset = ((41 * 39) * (100 + 11 * 9));
        
        StringBuilder builder = new(string.Empty);

        for (int i = 0; i < challenge.Length; i++)
        {
            builder.Append(ChangeChar(challenge[i], i, offset));
        }
        return builder.ToString();
    }
    private static char ChangeChar(char value, int position, int offset)
    {
        return Convert.ToChar(((value * position + offset) % 77) + 48);
    }
}
