using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace Kahoot.NET.Shared;

internal static class Extensions
{
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


        for (int i = 0; i < result.Length;)
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
