using System.Runtime.InteropServices;

namespace Kahoot.NET.API.Authentication;

/// <summary>
/// Static class to create the websocket key used for connection to the socket
/// </summary>
public static class WebSocketKey
{
    /// <summary>
    /// A sizeable amount of length for a session header to be, if the amount exceeds this <see cref="Create(string, string)"/> will throw <see cref="ArgumentOutOfRangeException"/>
    /// </summary>
    public const int MaxHeaderLength = 256;


    /// <summary>
    /// Decodes the strings and returns a websocket key
    /// </summary>
    /// <remarks>
    /// The header must be Base64 and comes from the response headers <see cref="Request.QueryGameAsync(HttpClient, int)"/> with the key <see cref="ConnectionInfo.SessionHeader"/>. 
    /// The challenge function comes from the response <see cref="Request.QueryGameAsync(HttpClient, int)"/> and is contained in the body. 
    /// The header cannot exceed the max length 
    /// </remarks>
    /// <param name="sessionHeader"></param>
    /// <param name="challengeFunction"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static string Create(string sessionHeader, string challengeFunction)
    {
        if (sessionHeader.Length % 4 != 0 || sessionHeader.Length > MaxHeaderLength)
        {
            throw new ArgumentException($"The header cannot exceed {MaxHeaderLength} and must be a multiple of 4", nameof(sessionHeader));
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
