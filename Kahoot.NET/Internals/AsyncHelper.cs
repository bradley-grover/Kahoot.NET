namespace Kahoot.NET.Internals;

// from https://github.com/aspnet/AspNetIdentity/blob/main/src/Microsoft.AspNet.Identity.Core/AsyncHelper.cs

internal static class AsyncHelper
{
    private static readonly TaskFactory _taskFactory = new(
        CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);
    public static void RunSync(Func<Task> func)
    {
        _taskFactory
            .StartNew(func)
            .Unwrap()
            .GetAwaiter()
            .GetResult();
    }
}
