using System.Diagnostics.Contracts;

namespace Kahoot.NET.API;

internal static class StringUtils
{
    /// <summary>
    /// Removes all the whitespace from a <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>
    /// </summary>
    /// <remarks>
    /// If <see href="value"/> is <see cref="ReadOnlySpan{T}.Empty"/> it will allocate an empty string
    /// </remarks>
    /// <param name="str">Input string</param>
    /// <returns>A <see cref="string"></see> with no whitespace characters</returns>
    [Pure]
    public static string RemoveWhiteSpace(this ReadOnlySpan<char> str)
    {
        if (str.IsEmpty)
        {
            return string.Empty;
        }

        char[] source = str.ToArray();

        int destinationIndex = 0;

        for (int i = 0; i < str.Length; i++)
        {
            char item = source[i];

            switch (item)
            {
                case '\u0020':
                case '\u00A0':
                case '\u1680':
                case '\u2000':
                case '\u2001':
                case '\u2002':
                case '\u2003':
                case '\u2004':
                case '\u2005':
                case '\u2006':
                case '\u2007':
                case '\u2008':
                case '\u2009':
                case '\u200A':
                case '\u202F':
                case '\u205F':
                case '\u3000':
                case '\u2028':
                case '\u2029':
                case '\u0009':
                case '\u000A':
                case '\u000B':
                case '\u000C':
                case '\u000D':
                case '\u0085':
                    continue;
                default:
                    source[destinationIndex++] = item;
                    break;
            }
        }

        return new string(source, 0, destinationIndex);
    }
}
