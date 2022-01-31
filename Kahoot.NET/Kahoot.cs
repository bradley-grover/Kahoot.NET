using System.Net.NetworkInformation;

namespace Kahoot.NET;

/// <summary>
/// Static methods that relate to Kahoot
/// </summary>
public static class Kahoot
{
    /// <summary>
    /// The public facing URL for Kahoot
    /// </summary>
    public const string PublicURL = "https://kahoot.it/";

    /// <summary>
    /// Pings Kahoot's <see cref="PublicURL"/> to see if it is online
    /// </summary>
    /// <param name="timeOut">The timout for the ping</param>
    /// <returns><see langword="true"/> if the website is online
    /// <see langword="false"/> if is offline</returns>
    public static bool IsOnline(int timeOut = 1000)
    {
        Ping ping = new();
        var reply = ping.Send(PublicURL, timeOut);

        return reply.Status.HasFlag(IPStatus.Success);
    }
    /// <summary>
    /// Pings Kahoot's <see cref="PublicURL"></see> asynchronously to see if it is online
    /// </summary>
    /// <param name="timeOut">The timeout for the ping</param>
    /// <returns><see langword="true"/> if the website is online
    /// <see langword="false"/> if is offline</returns>
    public static async Task<bool> IsOnlineAsync(int timeOut = 1000)
    {
        Ping ping = new();
        var reply = await ping.SendPingAsync(PublicURL, timeOut);

        return reply.Status.HasFlag(IPStatus.Success);
    }
}
