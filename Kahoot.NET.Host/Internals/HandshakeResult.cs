namespace Kahoot.NET.Host.Internals;

internal readonly struct SessionResult
{
    private readonly uint _code;
    private readonly string _key;

    internal SessionResult(uint code, string key)
    {
        _code = code;
        _key = key;
    }

    public bool IsValid => _code != 0;

    public uint Code => _code;

    public string Key => _key;

    public static SessionResult Invalid => default;
}
