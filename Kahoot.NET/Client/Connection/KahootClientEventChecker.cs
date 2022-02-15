using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kahoot.NET.Client.InternalConnectionState;
using Kahoot.NET.Internals.Messages.Handshake;

namespace Kahoot.NET.Client;

/// <inheritdoc></inheritdoc>
public partial class KahootClient
{
    private readonly CurrentSession _sessionState = new();

    internal async Task SendFirstMessageAsync(CancellationToken cancellationToken = default)
    {
        if (WebSocket is null)
        {
            throw new InvalidOperationException("Connection must be created first before sending the first shake");
        }

        await WebSocket.SendAsync(ClientSerializer.Serialize(new FirstHandshake()), WebSocketMessageType.Text, true, cancellationToken);

    }
}
