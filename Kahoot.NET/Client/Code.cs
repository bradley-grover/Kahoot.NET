namespace Kahoot.NET.Client;

/// <summary>
/// Has static methods to verify playability
/// </summary>
public static class Code
{
    internal const string GameLink = "https://kahoot.it?pin=";
    internal static readonly int GameLinkLength = GameLink.Length;

    /// <summary>
    /// Attempts to parse a game code from a game url invite
    /// </summary>
    /// <param name="chars"></param>
    /// <param name="code"></param>
    /// <returns>If the code was extracted sucessfully</returns>
    public static bool TryGetCode(ReadOnlySpan<char> chars, out int code)
    {
        code = default;

        if (!chars.Contains(GameLink, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (chars.Length <= GameLinkLength)
        {
            return false;
        }

        int takeAmount = 0;

        for (int i = GameLinkLength; i < chars.Length; i++)
        {
            if (!char.IsDigit(chars[i]))
            {
                break;
            }

            takeAmount++;
        }

        return int.TryParse(chars.Slice(GameLinkLength, takeAmount), out code);
    }
}
