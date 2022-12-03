using Jace;

namespace Kahoot.NET.API.Authentication;

internal static class Challenge
{
    // TODO: look into more math libaries that could support faster operations to yield less allocs or speed improvements

    internal static readonly CalculationEngine _engine = new();

    internal const string OffsetName = "var offset = ";

    public static int GetChallenge(ReadOnlySpan<char> challengeFunction, Span<char> chars)
    {
        ReadOnlySpan<char> token = FindToken(challengeFunction);

        Span<char> offsetString = stackalloc char[128];

        int writtenToOffsetString = GetOffsetString(challengeFunction, offsetString);

        offsetString = offsetString[..writtenToOffsetString];

        long offset = CalculateOffset(offsetString);

        return Decode(token, chars, offset);
    }

    internal const char Identifier = '\'';

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> FindToken(ReadOnlySpan<char> source)
    {
        int first = source.IndexOf(Identifier);
        int last = source.LastIndexOf(Identifier);

        return source.Slice(++first, last - first);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long CalculateOffset(ReadOnlySpan<char> str) => (long)_engine.Calculate(new(str));

    public static int GetOffsetString(ReadOnlySpan<char> input, Span<char> output)
    {
        // set input

        input = input[(input.IndexOf(OffsetName) + OffsetName.Length)..];

        input = input[..input.IndexOf(';')];

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
