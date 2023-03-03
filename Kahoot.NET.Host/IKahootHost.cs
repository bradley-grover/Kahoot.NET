using System.Diagnostics.CodeAnalysis;

namespace Kahoot.NET.Host;

/// <summary>
/// A client that hosts a Kahoot! quiz
/// </summary>
public interface IKahootHost : IDisposable
{
    bool IsConnected { get; }

    /// <summary>
    /// The current session, if any, check <see cref="IsConnected"/> to verify if there is a session
    /// </summary>
    [MemberNotNullWhen(true, nameof(IsConnected))]
    Quiz? Game { get; }

    Task<bool> CreateAsync(Uri quizUrl, GameConfiguration? config = default, CancellationToken cancellationToken = default);
}
