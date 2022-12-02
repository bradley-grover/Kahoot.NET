using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Jobs;
using Kahoot.NET.API.Authentication;

namespace Kahoot.NET.Benchmarks.ToRun;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[SimpleJob(RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net60, targetCount: 50)]
[SimpleJob(RunStrategy.Throughput, runtimeMoniker: RuntimeMoniker.Net70, targetCount: 50)]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class KeyCreationBenchmarks
{
    private const string ChallengeFunction = "decode.call(this, 'LILuzw5MdnKGByXfauCcwRk8FVEhbG4GisXISV463KUS6gXOROnjUoh3uQZlk8vrB5ElK0swOfup0bG7BNBZgS0zfMiahYEroPxE'); function decode(message) {var offset = ((67\t + 92\t *   73\t *   93)\t + (4\t *   92)); if( this \t . angular   . isString \t ( offset   ))\t console\t .   log\t (\"Offset derived as: {\", offset, \"}\"); return  _\t . \t replace\t ( message,/./g, function(char, position) {return String.fromCharCode((((char.charCodeAt(0)*position)+ offset ) % 77) + 48);});}";
    private const string SessionHeader = "Bw8EUT5OLglrCkd+NWdaUX9XQW4PY1tGAAplV1xAM1wLbQN+SjFHbVMIbmxRKV8sJWc+ezk8Cz4gDBlMbmlbMgsVLwU1CV0UURBSfT4LWW1de2YIYl5cBlJbQl5mXDFE";

    [Benchmark]
    public void CreateKey()
    {
        _ = Key.Create(SessionHeader, ChallengeFunction);
    }

    [Benchmark]
    public void CreateKey_Modified()
    {
        _ = WebSocketKey.Create(SessionHeader, ChallengeFunction);
    }
}
