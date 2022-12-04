using System.Runtime.InteropServices;

namespace Kahoot.NET.API.Authentication;

/// <summary>
/// Static class to create the websocket key used for connection to the socket
/// </summary>
public static class WebSocketKey
{
    /// <summary>
    /// Creates the WebSocket key used for connecting to the WebSocket
    /// </summary>
    /// <param name="sessionHeader">The header <see cref="ConnectionInfo.SessionHeader"/> from a request</param>
    /// <param name="challengeFunction"><see cref="SessionResponse.Challenge"/></param>
    /// <remarks>
    /// Combine with <see cref="ConnectionInfo.WebsocketUrl"/> and a game code to connect to the socket
    /// </remarks>
    public static string Create(string sessionHeader, string challengeFunction)
    {
        Span<char> header = stackalloc char[256]; // allocate a stack buffer to hold the data extracted from the header

        int written = Header.GetHeader(sessionHeader, header);

        header = header[..written]; // trim down to the written size

        // repeat for the challenge section

        Span<char> challenge = stackalloc char[256];

        int challengeWritten = Challenge.GetChallenge(challengeFunction, challenge);

        challenge = challenge[..challengeWritten];

        // loop using references to avoid bound checks

        ref char headerRef = ref MemoryMarshal.GetReference(header);
        ref char challengeRef = ref MemoryMarshal.GetReference(challenge);

        for (int i = 0; i < header.Length; i++) // the length of websocket key is the same size as the decoded header
        {
            // black magic stuff
            var character = (int)Unsafe.Add(ref headerRef, i);

            var mod = (int)Unsafe.Add(ref challengeRef, i % challenge.Length);

            var decoded = character ^ mod;

            Unsafe.Add(ref headerRef, i) = (char)(uint)decoded;
        }

        return new string(header); // allocate a new string from the span for the caller to use
    }
}
