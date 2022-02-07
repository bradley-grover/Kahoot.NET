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
}
