using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Benchmarks.Alternatives;

internal static class WebSocketKeyAlternatives
{
    public static string Create(string sessionHeader, string challengeFunction)
    {
        if (sessionHeader.Length % 4 != 0 || sessionHeader.Length > WebSocketKey.MaxHeaderLength)
        {
            throw new ArgumentException($"The header cannot exceed {WebSocketKey.MaxHeaderLength} and must be a multiple of 4", nameof(sessionHeader));
        }

        // header
        Span<char> header = stackalloc char[256];

        int written = Header.GetHeader(sessionHeader, header);

        header = header[..written]; // trim down to the written size

        // repeat for the challenge section

        Span<char> challenge = stackalloc char[256];

        int challengeWritten = Challenge.GetChallenge(challengeFunction, challenge);

        challenge = challenge[..challengeWritten];

        // loop using references to avoid bound checks

        return BitwiseOrSpans(header, challenge);
    }

    internal static string BitwiseOrSpans(Span<char> header, Span<char> challenge)
    {
        for (int i = 0; (uint)i < (uint)header.Length; i++)
        {
            int character = header[i];
            int mod = challenge[i];

            header[i] = (char)(uint)(character ^ mod);
        }

        return new string(header); // allocate a new string from the span for the caller to use
    }
}
