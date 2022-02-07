namespace Kahoot.NET.Shared;

/// <summary>
/// Asynchrounous event handler to await a handler
/// </summary>
/// <param name="sender">sender</param>
/// <param name="e">Event Args</param>
/// <returns>A <see cref="Task"/></returns>

public delegate Task AsyncEventHandler(object? sender, EventArgs e);
/// <summary>
/// Asynchrounous event handler to await a handler
/// </summary>
/// <typeparam name="TEventArgs">The type of <see cref="EventArgs"/></typeparam>
/// <param name="sender">sender</param>
/// <param name="e">Event Args to be passed</param>
/// <returns>A <see cref="Task"/></returns>
public delegate Task AsyncEventHandler<TEventArgs>(object? sender, TEventArgs e)
    where TEventArgs : EventArgs;
