using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Kahoot.NET.API.Authentication;

// this file contains the most performance impact on the WebSocketKey.Create method as it has parse a math string
// and has to a allocate a string from the span

internal static class Challenge
{
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
        Debug.Assert(!source.IsEmpty);

        int first = source.IndexOf(Identifier);
        int last = source.LastIndexOf(Identifier);

        return source.Slice(++first, last - first);
    }

    /// <summary>
    /// Calculates the offset using the current math calculator, Kahoot.NET.Mathemtics which has zero allocation for strings below a certain amount of characters
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
        Debug.Assert(!input.IsEmpty);
        Debug.Assert(!output.IsEmpty);

        // find the variable method declaration start index and set the span to start at the assignment

        input = input[(input.IndexOf(OffsetName) + OffsetName.Length)..];

        // then trim off the end to whe re the statement ends at the semi colon
        input = input[..input.IndexOf(';')];

        // copy all characters that aren't whitespace into the output span and return the amount of written
        // characters for the caller to access

        int outputIndex = 0;

        for (int i = 0; (uint)i < (uint)input.Length; i++)
        {
            char character = input[i];

            if (char.IsWhiteSpace(character)) continue;

            output[outputIndex++] = character;
        }

        return outputIndex;
    }

    /// <summary>
    /// Decode the characters using the offset and store the characters in the output
    /// </summary>
    internal static int Decode(ReadOnlySpan<char> input, Span<char> output, long offset)
    {
        Debug.Assert(!(input.Length > ushort.MaxValue));

#if NET7_0_OR_GREATER
        if (offset <= ushort.MaxValue)
        {
            return FastDecode(input, output, (ushort)offset);
        }
#endif

        for (ushort i = 0; i < input.Length; i++)
        {
            // add decoded characters to span
            output[i] = Repl(input[i], i, offset);
        }

        return input.Length;
    }

#if NET7_0_OR_GREATER
    internal static int FastDecode(ReadOnlySpan<char> input, Span<char> output, ushort offset)
    {
        ushort i = 0;

        if (Vector256.IsHardwareAccelerated && input.Length >= Vector256<ushort>.Count)
        {
            ReadOnlySpan<ushort> ushorts = MemoryMarshal.Cast<char, ushort>(input);
            Span<ushort> outputAsUshort = MemoryMarshal.Cast<char, ushort>(output);

            var offsetVec = Vector256.Create<ushort>(offset);
            var modulo = Vector256.Create<ushort>(77);
            var addition = Vector256.Create<ushort>(48);

            for (; i <= input.Length - Vector256<ushort>.Count; i += (ushort)Vector256<ushort>.Count)
            {
                var indices = Vector256.Create(i, (ushort)(i + 1), 
                    (ushort)(i + 2), (ushort)(i + 3),
                    (ushort)(i + 4), (ushort)(i + 5), 
                    (ushort)(i + 6), (ushort)(i + 7), 
                    (ushort)(i + 8), (ushort)(i + 9), 
                    (ushort)(i + 10), (ushort)(i + 11), 
                    (ushort)(i + 12), (ushort)(i + 13), 
                    (ushort)(i + 14), (ushort)(i + 15));

                var characters = Vector256.Create(ushorts.Slice(i, Vector256<ushort>.Count));

                var result = Vector256.Multiply(characters, indices);

                result += offsetVec;

                var a = result - modulo;

                var b = (result / modulo);

                result = a * b;

                result += addition;

                result.CopyTo(outputAsUshort.Slice(i, Vector256<ushort>.Count));
            }
        }

        for (; i < input.Length; i++)
        {
            // add decoded characters to span
            output[i] = Repl(input[i], i, offset);
        }

        return input.Length;
    }
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static char Repl(char character, int position, long offset)
    {
        return (char)(ulong)(((character * position + offset) % 77) + 48);
    }
}
