using System.Diagnostics.Contracts;

namespace Kahoot.NET.Shared;

/// <summary>
/// Static methods that are project wide
/// </summary>
internal static class Utils
{
    /// <summary>
    /// Highly optimized way of removing white space from span prefer over string version
    /// </summary>
    /// <remarks>
    /// This method works about 2x faster and is about 8x more memory efficient
    /// than <see cref="ChallengeToken.RemoveWhitespace(string)"/>
    /// </remarks>
    /// <param name="input"></param>
    /// <returns><see cref="ReadOnlySpan{T}"/> where the spaces are removed</returns>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ReadOnlySpan<char> RemoveWhitespace(this ReadOnlySpan<char> input)
    {
        if (input.IsEmpty)
        {
            return input;
        }

        int toIgnore = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsWhiteSpace(input[i]))
            {
                toIgnore++;
            }
        }

        if (toIgnore == 0)
        {
            return input;
        }

        int newSize = input.Length - toIgnore;

        Span<char> result = new char[newSize];

        int indexesAhead = 0;
        int originalSpan = 0;


        for (int i = 0; i < result.Length; )
        {
            if (char.IsWhiteSpace(input[indexesAhead + originalSpan]))
            {
                indexesAhead++;
                continue;
            }
            result[i++] = input[(originalSpan++) + indexesAhead];
        }


        return result;
    }
}
