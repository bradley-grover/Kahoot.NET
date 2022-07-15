namespace Kahoot.NET.Hosting.Json;

[JsonSerializable(typeof(GameConfiguration))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class GameConfigurationContext : JsonSerializerContext
{

}
