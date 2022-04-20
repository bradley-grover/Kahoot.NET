using Kahoot.NET.Internal.Data.Messages.Handshake;

namespace Kahoot.NET.Internal.Data.SourceGenerators.Messages;

[JsonSerializable(typeof(LiveClientHandshake))]
internal partial class LiveClientHandshakeContext : JsonSerializerContext
{
}
