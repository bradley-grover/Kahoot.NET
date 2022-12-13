using System.Runtime.InteropServices;
using Jace;
using Kahoot.NET.Mathematics;

namespace Kahoot.NET.API.Authentication;

// this file contains the most performance impact on the WebSocketKey.Create method as it has parse a math string
// and has to a allocate a string from the span

internal static class Challenge
{
    // TODO: look into more math libaries that could support faster operations to yield less allocs or speed improvements

    internal static readonly CalculationEngine _engine = new(); // calculation engine from Jace.NET to eval the string


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
    
    /// <summary>
    /// Finds the token and creates a new span over it, the token is always encased between two <see cref="Identifier"/>
    /// characters so we just have to find the indexes. The span also never contains any other <see cref="Identifier"/>
    /// so it should always produce the same result
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> FindToken(ReadOnlySpan<char> source)
    {
        int first = source.IndexOf(Identifier);
        int last = source.LastIndexOf(Identifier);

        return source.Slice(++first, last - first);
    }

    /// <summary>
    /// Calculates the offset from the offset string, this method by far has the most allocations and slows down the 
    /// decoding of the websocket key the most, a lightweight alternative would be preffered
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long CalculateOffset(ReadOnlySpan<char> str) => SimpleExpression.Evaluate(str);

    /// <summary>
    /// Finds the offset string to calculate and puts the characters in the output span because they are not in order
    /// </summary>
    public static int GetOffsetString(ReadOnlySpan<char> input, Span<char> output)
    {
        // find the variable method declaration start index and set the span to start at the assignment

        input = input[(input.IndexOf(OffsetName) + OffsetName.Length)..];

        // then trim off the end to where the statement ends at the semi colon
        input = input[..input.IndexOf(';')];

        // copy all characters that aren't whitespace into the output span and return the amount of written
        // characters for the caller to access

        int outputIndex = 0;

        ref char inputRef = ref MemoryMarshal.GetReference(input);
        ref char outputRef = ref MemoryMarshal.GetReference(output);

        for (int i = 0; i < input.Length; i++)
        {
            char character = Unsafe.Add(ref inputRef, i);

            if (char.IsWhiteSpace(character))
            {
                continue;
            }

            Unsafe.Add(ref outputRef, outputIndex++) = character;
        }

        return outputIndex;
    }

    /// <summary>
    /// Decode the characters using the offset and store the characters in the output
    /// </summary>
    internal static int Decode(ReadOnlySpan<char> value, Span<char> output, long offset)
    {
        for (int i = 0; i < value.Length; i++)
        {
            // add decoded characters to span
            output[i] = Repl(value[i], i, offset);
        }

        return value.Length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static char Repl(char character, int position, long offset)
    {
        return (char)(ulong)(((character * position + offset) % 77) + 48);
    }
}
