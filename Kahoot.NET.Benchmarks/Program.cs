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
