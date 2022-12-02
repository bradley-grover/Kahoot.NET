/* 
 * Kahoot.NET 
 * Benchmarks
 * 
 * To create a benchmark see the benchmark below main
 */

using BenchmarkDotNet.Running;
using Kahoot.NET.API.Authentication;
using Kahoot.NET.API.Authentication.Token;
using Kahoot.NET.Benchmarks.ToRun;

namespace Kahoot.NET.Benchmarks;

public class Program
{
    private const string ChallengeFunction = "decode.call(this, 'LILuzw5MdnKGByXfauCcwRk8FVEhbG4GisXISV463KUS6gXOROnjUoh3uQZlk8vrB5ElK0swOfup0bG7BNBZgS0zfMiahYEroPxE'); function decode(message) {var offset = ((67\t + 92\t *   73\t *   93)\t + (4\t *   92)); if( this \t . angular   . isString \t ( offset   ))\t console\t .   log\t (\"Offset derived as: {\", offset, \"}\"); return  _\t . \t replace\t ( message,/./g, function(char, position) {return String.fromCharCode((((char.charCodeAt(0)*position)+ offset ) % 77) + 48);});}";
    private const string SessionHeader = "Bw8EUT5OLglrCkd+NWdaUX9XQW4PY1tGAAplV1xAM1wLbQN+SjFHbVMIbmxRKV8sJWc+ezk8Cz4gDBlMbmlbMgsVLwU1CV0UURBSfT4LWW1de2YIYl5cBlJbQl5mXDFE";
    public static void Main(string[] args)
    {

        //BenchmarkRunner.Run<DecodeBenchmarks>();
        //BenchmarkRunner.Run<KeyCreationBenchmarks>();
        BenchmarkRunner.Run<WebSocketKeyBenchmarks>();
    }
    /*
     * using Kahoot.NET.[X]
     * 
     * namespace Kahoot.NET.Benchmarks.[X]
     * 
     * [MemoryDiagnoser]
     * public class [X]Benchmarks
     * {
     *     //put result of benchmark above the attribute like this
     *     // [date] | [median] | [memoryUsage]
     *     // eg
     *     
     *     // 22-02-04 | 20us | 200B
     *     
     *     [Benchmark(Baseline = true)
     *     public void MethodOne()
     *     {
     *         // do stuff
     *     }
     *     [Benchmark]
     *     public void MethodTwo()
     *     {
     *        // do stuff
     *     }
     * }
     */
}
