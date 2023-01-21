namespace Kahoot.NET.CLI;

/// <summary>
/// Provides methods to run asymc method as sync in the safest way possible
/// </summary>
public static class Async
{
    private static readonly TaskFactory _factory = new(
        CancellationToken.None,
        TaskCreationOptions.None,
        TaskContinuationOptions.None,
        TaskScheduler.Default);

    /// <summary>
    /// Runs a function that returns a Task of T synchronously
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="func"></param>
    /// <returns></returns>
    public static TResult RunSync<TResult>(Func<Task<TResult>> func)
    {
        return _factory
            .StartNew(func)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }

    /// <summary>
    /// Runs a async functions in sync
    /// </summary>
    /// <param name="func"></param>
    public static void RunSync(Func<Task> func)
    {
        _factory
            .StartNew(func)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }
}
