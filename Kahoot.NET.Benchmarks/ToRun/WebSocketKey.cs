using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Kahoot.NET.API.Authentication;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.PortableExecutable;

namespace Kahoot.NET.Benchmarks.ToRun;

[BenchmarkModule("KeyCreationSteps", "Benchmarks the methods involved with creating the websocket key used to connect to a Kahoot!")]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class WebSocketKeyBenchmarks
{
    [Benchmark]
    public void Key_Create()
    {
        _ = WebSocketKey.Create(Mock.SessionHeader, Mock.ChallengeFunction);
    }

    [Benchmark]
    public void Get_Header()
    {
        // from WebSoccketKey.Create(str, str);

        Span<char> header = stackalloc char[256];

        int written = Header.GetHeader(Mock.SessionHeader, header);

        header = header[..written];
    }

    [Benchmark]
    public void Get_Challenge()
    {
        /// from WebSocketKey.Create(str, str)

        Span<char> challenge = stackalloc char[256];

        int challengeWritten = Challenge.GetChallenge(Mock.ChallengeFunction, challenge);

        challenge = challenge[..challengeWritten];
    }

    [Benchmark]
    public void Get_OffsetString()
    {
        Span<char> offsetString = stackalloc char[128];

        int writtenToOffsetString = Challenge.GetOffsetString(Mock.ChallengeFunction, offsetString);

        offsetString = offsetString[..writtenToOffsetString];
    }

    [Benchmark]
    public void Calculate_Offset()
    {
        _ = Challenge.CalculateOffset(Mock.OffsetString);
    }

    [Benchmark]
    public void Find_Token()
    {
        _ = Challenge.FindToken(Mock.ChallengeFunction);
    }

    [Benchmark]
    public void Decode()
    {
        Span<char> bytes = stackalloc char[256];

        int written = Challenge.Decode(Mock.Token, bytes, Mock.Offset); // pre-calculated

        bytes = bytes[..written];
    }
}
