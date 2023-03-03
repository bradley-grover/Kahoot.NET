using System.Text.Json.Serialization;

namespace Kahoot.NET.Host.Json;

[JsonSerializable(typeof(GameConfiguration))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class GameConfigurationContext : JsonSerializerContext
{

}
