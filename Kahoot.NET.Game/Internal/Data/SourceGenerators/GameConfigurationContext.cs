namespace Kahoot.NET.Game.Internal.Data.SourceGenerators;

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(GameConfiguration))]
internal partial class GameConfigurationContext : JsonSerializerContext
{
}
