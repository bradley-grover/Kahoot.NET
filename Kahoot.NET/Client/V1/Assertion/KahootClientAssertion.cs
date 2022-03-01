namespace Kahoot.NET.Client;

/// <summary>
/// 
/// </summary>
public partial class KahootClient
{
    internal void AssertConnected()
    {
        if (Socket is null || Socket.State is not WebSocketState.Open)
        {
            throw new InvalidOperationException("Connection is not open for this");
        }
    }
}
