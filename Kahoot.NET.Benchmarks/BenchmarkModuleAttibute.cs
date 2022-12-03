namespace Kahoot.NET.Benchmarks;

[AttributeUsage(AttributeTargets.Class)]
public sealed class BenchmarkModuleAttribute : Attribute
{
    public string Title { get; }

    public string Description { get; }

    public BenchmarkModuleAttribute(string title, string description)
    {
        ArgumentNullException.ThrowIfNull(title);
        ArgumentNullException.ThrowIfNull(description);

        Title = title;
        Description = description;
    }
}
