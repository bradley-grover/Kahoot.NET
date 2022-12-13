namespace Kahoot.NET.Extensions;

internal static class UriHelper
{
    public static string CreateGameUrl(uint gameId, string webSocketKey)
    {
        Span<char> chars = stackalloc char[11]; // an integer uses only 10 digits but lets add the '/' at the end so we dont have to add another character

        gameId.TryFormat(chars, out int written);

        chars = chars[..(written + 1)];

        chars[written] = '/';

        return string.Concat(ConnectionInfo.WebSocketUrlNoFormat, chars, webSocketKey);
    }
}
