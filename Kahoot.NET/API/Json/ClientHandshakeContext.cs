using Kahoot.NET.API.Requests.Handshake;

namespace Kahoot.NET.API.Requests.Json;

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true)]
[JsonSerializable(typeof(ClientHandshake))]
internal partial class ClientHandshakeContext : JsonSerializerContext
{

}
