using System.Buffers.Text;
using System.Runtime.InteropServices;
using Kahoot.NET.API.Authentication.Token;
using Kahoot.NET.Parsers;

namespace Kahoot.NET.API.Authentication;

public static class WebSocketKey
{
    public static string Create(string sessionHeader, string challengeFunction)
    {
        // header
        Span<char> header = stackalloc char[256];

        int written = GetHeader(sessionHeader, header);

        header = header[..written];

        Span<char> challenge = stackalloc char[256];

        int challengeWritten = GetChallenge(challengeFunction, challenge);

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

    public static int GetHeader(ReadOnlySpan<char> sessionHeader, Span<char> chars)
    {
        Span<byte> bytes = stackalloc byte[512];

        _ = Convert.TryFromBase64Chars(sessionHeader, bytes, out int written);

        return Encoding.UTF8.GetChars(bytes[..written], chars);
    }

    internal static readonly TokenIdentifer _identifier = new();
    internal static readonly OffsetArithmetic _calc = new();

    public static int GetChallenge(ReadOnlySpan<char> challengeFunction, Span<char> chars)
    {
        ReadOnlySpan<char> token = _identifier.Parse(challengeFunction);

        Span<char> offsetString = stackalloc char[128];

        int writtenToOffsetString = GetOffsetString(challengeFunction, offsetString);

        offsetString = offsetString[..writtenToOffsetString];

        long offset = _calc.Parse(offsetString);

        return Decode(token, chars, offset);
    }

    internal const string OffsetName = "var offset = ";

    public static int GetOffsetString(ReadOnlySpan<char> input, Span<char> output)
    {
        // set input

        input = input.Slice(input.IndexOf(OffsetName) + OffsetName.Length);

        input = input.Slice(0, input.IndexOf(';'));

        // end

        int outputIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char character = input[i];

            if (char.IsWhiteSpace(character))
            {
                continue;
            }

            output[outputIndex++] = character;
        }

        return outputIndex;
    }

    internal static int Decode(ReadOnlySpan<char> value, Span<char> output, long offset)
    {
        for (int i = 0; i < value.Length; i++)
        {
            // add decoded characters to span
            output[i] = Repl(value[i], i, offset);
        }

        return value.Length;
    }

    internal static char Repl(char character, int position, long offset)
    {
        int combined = character * position;

        // use kahoots challenge method to decode it
        long result = ((combined + offset) % 77) + 48;

        // convert back to a character
        return (char)(ulong)result;
    }
}
