namespace Kahoot.NET;

internal static class Extensions
{
    internal static string RemoveBrackets(this ReadOnlySpan<char> span)
    {
        int start = 1;
        int end = span.LastIndexOf(']');

        return span.Slice(start, end - 1).ToString();
    }

    /// <summary>
    /// Invokes an event if there are callers assigned to it
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="event"></param>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    internal static async Task InvokeEventAsync<TEventArgs>(
        this Func<object?, TEventArgs, Task>? @event, object? sender, TEventArgs args)
    {
        if (@event is not null)
        {
            await @event(sender, args);
        }
    }
}
