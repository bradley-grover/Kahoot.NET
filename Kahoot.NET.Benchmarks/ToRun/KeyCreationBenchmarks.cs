using System.Diagnostics.CodeAnalysis;
using Kahoot.NET.API.Authentication;
using Kahoot.NET.Benchmarks.Alternatives;

namespace Kahoot.NET.Benchmarks.ToRun;

[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Benchmark class, has to be instance methods")]
public class KeyCreationBenchmarks
{
    private const string ChallengeFunction = "decode.call(this, 'LILuzw5MdnKGByXfauCcwRk8FVEhbG4GisXISV463KUS6gXOROnjUoh3uQZlk8vrB5ElK0swOfup0bG7BNBZgS0zfMiahYEroPxE'); function decode(message) {var offset = ((67\t + 92\t *   73\t *   93)\t + (4\t *   92)); if( this \t . angular   . isString \t ( offset   ))\t console\t .   log\t (\"Offset derived as: {\", offset, \"}\"); return  _\t . \t replace\t ( message,/./g, function(char, position) {return String.fromCharCode((((char.charCodeAt(0)*position)+ offset ) % 77) + 48);});}";
    private const string SessionHeader = "Bw8EUT5OLglrCkd+NWdaUX9XQW4PY1tGAAplV1xAM1wLbQN+SjFHbVMIbmxRKV8sJWc+ezk8Cz4gDBlMbmlbMgsVLwU1CV0UURBSfT4LWW1de2YIYl5cBlJbQl5mXDFE";

    [Benchmark]
    public void CreateKey()
    {
        _ = WebSocketKey.Create(SessionHeader, ChallengeFunction);
    }

    [Benchmark]
    public void NoVectorization()
    {
        _ = WebSocketKeyAlternatives.Create(SessionHeader, ChallengeFunction);
    }
}
