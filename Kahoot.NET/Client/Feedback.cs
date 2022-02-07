namespace Kahoot.NET.Client;

/// <summary>
/// Feedback to send to Kahoot after the quiz is done
/// </summary>
public class Feedback
{
    public byte Fun { get; set; }
    public bool Learned { get; set; }
    public bool WouldRecommend { get; set; }
    public int Overall { get; set; }
}
