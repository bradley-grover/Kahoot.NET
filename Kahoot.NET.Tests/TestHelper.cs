using System.IO;
using Kahoot.NET.Client;
using Microsoft.Extensions.Configuration;

namespace Kahoot.NET.Tests;

public class TestHelper
{
    public static IConfiguration Configuration { get; } = GetConfig();

    public static IConfiguration GetConfig()
    {
        IConfigurationRoot root = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        return root;
    }

    public static IKahootClient LatestClient()
    {
        return new KahootClient();
    }
}
