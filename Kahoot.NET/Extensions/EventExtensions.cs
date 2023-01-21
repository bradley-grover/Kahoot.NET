namespace Kahoot.NET.Extensions;

internal static class EventExtensions
{
    /// <summary>
    /// Invokes an event if there are callers assigned to it
    /// </summary>
    /// <typeparam name="TEventArgs"></typeparam>
    /// <param name="event">The event to invoke</param>
    /// <param name="sender">The sender of the event</param>
    /// <param name="args">The event args/data to pass</param>
    /// <returns>A <see cref="Task"/> to <see langword="await"/></returns>
    internal static async Task InvokeEventAsync<TEventArgs>(
        this Func<object?, TEventArgs, Task>? @event, object? sender, TEventArgs args)
        where TEventArgs : EventArgs
    {
        Debug.Assert(args != null);

        if (@event is not null)
        {
            await @event(sender, args);
        }
    }
}
