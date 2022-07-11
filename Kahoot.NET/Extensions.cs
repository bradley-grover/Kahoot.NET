namespace Kahoot.NET;

internal static class Extensions
{
    internal static string RemoveBrackets(this ReadOnlySpan<char> span)
    {
        int start = 1;
        int end = span.LastIndexOf(']');

        return span.Slice(start, end - 1).ToString();
    }
}
