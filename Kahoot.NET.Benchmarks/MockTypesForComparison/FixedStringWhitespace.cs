namespace Kahoot.NET.Benchmarks.MockTypesForComparison;

public static class FixedStringWhitespace
{
    public static unsafe string RemoveWhitespaceM(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        int toIgnore = 0;

        fixed (char* p = input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(p[i]))
                {
                    toIgnore++;
                }
            }
        }

        if (toIgnore == 0)
        {
            return input;
        }

        int newSize = input.Length - toIgnore;

        Span<char> result = stackalloc char[newSize];

        int indexesAhead = 0;
        int originalSpan = 0;


        for (int i = 0; i < newSize;)
        {
            if (char.IsWhiteSpace(input[indexesAhead + originalSpan]))
            {
                indexesAhead++;
                continue;
            }
            result[i++] = input[(originalSpan++) + indexesAhead];
        }

        return new string(result);
    }
    public static string RemoveInPlaceCharArray(this string input)
    {
        var len = input.Length;
        var src = input.ToCharArray();
        int dstIdx = 0;
        for (int i = 0; i < len; i++)
        {
            var ch = src[i];
            switch (ch)
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
                    src[dstIdx++] = ch;
                    break;
            }
        }
        return new string(src, 0, dstIdx);
    }
}
