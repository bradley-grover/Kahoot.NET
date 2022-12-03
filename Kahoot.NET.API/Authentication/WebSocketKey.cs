using System.Runtime.InteropServices;

namespace Kahoot.NET.API.Authentication;

public static class WebSocketKey
{
    public static string Create(string sessionHeader, string challengeFunction)
    {
        // header
        Span<char> header = stackalloc char[256];

        int written = Header.GetHeader(sessionHeader, header);

        header = header[..written];

        Span<char> challenge = stackalloc char[256];

        int challengeWritten = Challenge.GetChallenge(challengeFunction, challenge);

        challenge = challenge[..challengeWritten];

        // enumerate using refs to eliminate bound checks

        ref char headerRef = ref MemoryMarshal.GetReference(header);
        ref char challengeRef = ref MemoryMarshal.GetReference(challenge);

        for (var i = 0; i < header.Length; i++) // loop every character in the header
        {
            // black magic stuff
            var character = (int)Unsafe.Add(ref headerRef, i);

            var mod = (int)Unsafe.Add(ref challengeRef, i % challenge.Length);

            var decoded = character ^ mod;

            Unsafe.Add(ref headerRef, i) = Convert.ToChar(decoded);
        }

        return new string(header); // allocate into a string
    }
}
