using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

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

        return BitwiseOrSpans(header, challenge);
    }

#if !NET7_0_OR_GREATER
    internal static string BitwiseOrSpans(Span<char> header, Span<char> challenge)
    {
        if (!(challenge.Length >= header.Length))
        {
            throw new ArgumentException("The challenge length should result in same or higher length");
        }

        int i = 0;

        if (Vector.IsHardwareAccelerated && header.Length >= Vector<ushort>.Count)
        {
            int length = header.Length;
            int vectorLength = Vector<ushort>.Count;

            // we need to use unsigned shorts as chars are not supported by vectors

            Span<ushort> headerRaw = MemoryMarshal.Cast<char, ushort>(header);
            Span<ushort> challengeRaw = MemoryMarshal.Cast<char, ushort>(challenge);

            for (i = 0; i <= length - vectorLength; i += vectorLength)
            {
                var headerVec = new Vector<ushort>(headerRaw.Slice(i, vectorLength));
                var challengeVec = new Vector<ushort>(challengeRaw.Slice(i, vectorLength));

                headerVec = Vector.Xor(headerVec, challengeVec);

                headerVec.CopyTo(headerRaw.Slice(i, vectorLength));
            }
        }

        // process the remaining or unvectorized part of the function

        for (; (uint)i < (uint)header.Length; i++)
        {
            int character = header[i];
            int mod = challenge[i];

            header[i] = (char)(uint)(character ^ mod);
        }

        return new string(header); // allocate a new string from the span for the caller to use
    }
#else
    internal static string BitwiseOrSpans(Span<char> header, Span<char> challenge)
    {
        if (!(challenge.Length >= header.Length))
        {
            throw new ArgumentException("The challenge length should result in same or higher length");
        }

        int i = 0;

        if (Vector256.IsHardwareAccelerated && header.Length >= Vector256<ushort>.Count)
        {
            int length = header.Length;
            int vectorLength = Vector256<ushort>.Count;

            // we need to use unsigned shorts as chars are not supported by vectors

            Span<ushort> headerRaw = MemoryMarshal.Cast<char, ushort>(header);
            Span<ushort> challengeRaw = MemoryMarshal.Cast<char, ushort>(challenge);

            ref ushort headerRawReference = ref MemoryMarshal.GetReference(headerRaw);
            ref ushort challengeRawReference = ref MemoryMarshal.GetReference(challengeRaw);

            for (i = 0; i <= length - vectorLength; i += vectorLength)
            {
                var headerVector = Vector256.LoadUnsafe(ref headerRawReference, (nuint)i);
                var challengeVector = Vector256.LoadUnsafe(ref challengeRawReference, (nuint)i);

                headerVector = Vector256.Xor(headerVector, challengeVector);

                headerVector.CopyTo(headerRaw.Slice(i, vectorLength));
            }
        }

        // Process the remainder of the elements using the original loop
        for (; (uint)i < (uint)header.Length; i++)
        {
            int character = header[i];
            int mod = challenge[i];
            header[i] = (char)(uint)(character ^ mod);
        }

        return new string(header); // allocate a new string from the span for the caller to use
    }
#endif
}
