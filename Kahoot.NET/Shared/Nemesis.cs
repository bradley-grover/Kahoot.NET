namespace Kahoot.NET.Shared;

public class Nemesis : INemesis
{
    public string? Name { get; set; }
    public int? Score { get; set; } = 0;

    public bool? IsGhost { get; set; }
}
