using System.Net.NetworkInformation;

namespace Kahoot.NET;

/// <summary>
/// Static methods that relate to Kahoot
/// </summary>
public static class Kahoot
{
    internal static HttpClient? Client { get; set; }
    /// <summary>
    /// Sets the global <see cref="HttpClient"/> to be used for the <see cref="KahootClient"/>
    /// internal messaging
    /// </summary>
    /// <param name="client"></param>
    public static void SetGlobalHttpClient(HttpClient client)
    {
        ArgumentNullException.ThrowIfNull(client, nameof(client));
        Client = client;
    }
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
    /// <summary>
    /// Creates the most recent <see cref="IKahootClient"/> implementation
    /// </summary>
    /// <returns></returns>
    public static IKahootClient CreateClient()
    {
        return new KahootClient(KahootClientConfig.Default, new HttpClient());
    }

    /// <summary>
    /// Creates the most recent <see cref="IKahootClient"/> implementation with the specified configuration
    /// </summary>
    /// <param name="config"></param>
    /// <returns></returns>
    public static IKahootClient CreateClient(KahootClientConfig config)
    {
        return new KahootClient(config, new HttpClient());
    }

    /// <summary>
    /// Creates many <see cref="IKahootClient"/> using the integer passed
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public static IEnumerable<IKahootClient> CreateClients(int amount)
    {
        ThrowHelper.AssertAboveZero(amount);

        for (int i = 0; i < amount; i++)
        {
            yield return CreateClient();
        }
    }
}
