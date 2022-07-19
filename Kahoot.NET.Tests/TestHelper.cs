using System.IO;
using Kahoot.NET.Client;
using Kahoot.NET.Hosting.Client;
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

    public static IKahootClient Latest_Client()
    {
        return new KahootClient();
    }

    public static IKahootHost Create_Host()
    {
        return new KahootHost();
    }


}
