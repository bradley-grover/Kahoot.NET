namespace Kahoot.NET.Client.Events;

#nullable disable

/// <summary>
/// Event data for when quiz has started
/// </summary>
public class QuizStartedEventArgs : EventArgs
{
    /// <summary>
    /// The quiz title
    /// </summary>
    public string QuizTitle { get; internal set; }
}
