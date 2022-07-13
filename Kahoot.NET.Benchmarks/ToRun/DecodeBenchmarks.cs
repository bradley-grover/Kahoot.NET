using System.Diagnostics.CodeAnalysis;
using BenchmarkDotNet.Engines;
using Kahoot.NET.API.Authentication.Token;
using Kahoot.NET.Parsers;

namespace Kahoot.NET.Benchmarks.ToRun;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(RunStrategy.Throughput, targetCount: 50)]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class DecodeBenchmarks
{
    private const string ChallengeFunction = "decode.call(this, 'LILuzw5MdnKGByXfauCcwRk8FVEhbG4GisXISV463KUS6gXOROnjUoh3uQZlk8vrB5ElK0swOfup0bG7BNBZgS0zfMiahYEroPxE'); function decode(message) {var offset = ((67\t + 92\t *   73\t *   93)\t + (4\t *   92)); if( this \t . angular   . isString \t ( offset   ))\t console\t .   log\t (\"Offset derived as: {\", offset, \"}\"); return  _\t . \t replace\t ( message,/./g, function(char, position) {return String.fromCharCode((((char.charCodeAt(0)*position)+ offset ) % 77) + 48);});}";
    private const string SessionHeader = "Bw8EUT5OLglrCkd+NWdaUX9XQW4PY1tGAAplV1xAM1wLbQN+SjFHbVMIbmxRKV8sJWc+ezk8Cz4gDBlMbmlbMgsVLwU1CV0UURBSfT4LWW1de2YIYl5cBlJbQl5mXDFE";

    [Benchmark]
    public void Create_Token()
    {
        _ = Key.Create(SessionHeader, ChallengeFunction);
    }

    [Benchmark]
    public void Challenge_Portion()
    {
        _ = Challenge.CreateToken(ChallengeFunction);
    }

    [Benchmark]
    public void Header_Function()
    {
        _ = Header.Create(SessionHeader);
    }

    [Benchmark]
    public void Finding_Offset()
    {
        var offsetFinder = new OffsetIdentifer();

        _ = offsetFinder.Parse(ChallengeFunction.AsSpan().RemoveWhiteSpace());
    }

    [Benchmark]
    public void Find_And_CalcOffset()
    {
        var offsetFinder = new OffsetIdentifer();

        var offsetString = offsetFinder.Parse(ChallengeFunction.AsSpan().RemoveWhiteSpace());

        var offsetCalculator = new OffsetArithmetic();

        _ = offsetCalculator.Parse(offsetString);
    }

    [Benchmark]
    public void Calculate_Offset()
    {
        var offsetCalculator = new OffsetArithmetic();

        _ = offsetCalculator.Parse("((67+92*73*93)+(4*92))");
    }

    [Benchmark]
    public void Decode()
    {
        _ = Challenge.Decode("LILuzw5MdnKGByXfauCcwRk8FVEhbG4GisXISV463KUS6gXOROnjUoh3uQZlk8vrB5ElK0swOfup0bG7BNBZgS0zfMiahYEroPxE",
            625023);
    }
}
