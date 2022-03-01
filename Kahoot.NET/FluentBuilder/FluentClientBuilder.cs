namespace Kahoot.NET.FluentBuilder;

/// <summary>
/// Fluent builder for <see cref="KahootClient"/>
/// </summary>
public class FluentClientBuilder
{
    private HttpClient? _httpClient;
    private ILogger<IKahootClient>? _logger;
    private FluentClientBuilder()
    {
        
    }
    /// <summary>
    /// Creates a <see cref="FluentClientBuilder"/> to be chained
    /// </summary>
    /// <returns>A <see cref="FluentClientBuilder"/> to be chained</returns>
    public static FluentClientBuilder New()
    {
        return new();
    }
    /// <summary>
    /// Adds a <see cref="ILogger{TCategoryName}"/> using a <see cref="Func{T, TResult}"/> delegate
    /// </summary>
    /// <param name="funcLogger"></param>
    /// <returns>A <see cref="FluentClientBuilder"/> to be chainged</returns>
    public FluentClientBuilder WithLogger(Func<ILogger<IKahootClient>> funcLogger)
    {
        _logger = funcLogger();
        return this;
    }
    /// <summary>
    /// Passes a <see cref="ILogger{TCategoryName}"/> to the client
    /// </summary>
    /// <param name="logger"></param>
    /// <returns></returns>
    public FluentClientBuilder WithLogger(ILogger<IKahootClient> logger)
    {
        _logger = logger;
        return this;
    }
    /// <summary>
    /// Passes a <see cref="HttpClient"/> to be used for the <see cref="KahootClient"/>
    /// </summary>
    /// <param name="client"></param>
    /// <returns></returns>
    public FluentClientBuilder WithHttpClient(HttpClient client)
    {
        _httpClient = client;
        return this;
    }
    /// <summary>
    /// Builds the <see cref="FluentClientBuilder"/>
    /// </summary>
    /// <returns>A <see cref="KahootClient"/></returns>
    public KahootClient Build()
    {
        return new KahootClient(_logger!, _httpClient!);
    }
}
