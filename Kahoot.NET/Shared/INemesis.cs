namespace Kahoot.NET.Shared;

public interface INemesis
{
    bool? IsGhost { get; set; }
    string? Name { get; set; }
    int? Score { get; set; }
}
