using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.API;
using Kahoot.NET.Extensions;

namespace Kahoot.NET.Benchmarks.ToRun;

[BenchmarkModule("GameUrl", "Comparing URL performances")]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class GameUrlCreationBenchmarks
{
    // need a test key for this
    internal const string Key = "9588f8f77907a8de5f018910020537a7d9026f647145bd5cb4a5da2ae7b453da8bf9e65bed28e5a2792780148e4148a4";
    internal const int Code = 543_147; // codes are usually around 

    [Benchmark]
    public string Create_GameUrl()
    {
        return string.Format(ConnectionInfo.WebsocketUrl, Code, Key);
    }

    [Benchmark]
    public string Create_GameUrlHelper()
    {
        return UriHelper.CreateGameUrl(Code, Key);
    }
}
