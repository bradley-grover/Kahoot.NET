using System.Collections;

namespace Kahoot.NET.Tests.DataSources;

public class Expressions : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var lines = File.ReadAllLines("Expressions.txt");

        if (!lines.Any())
        {
            throw new Exception("Expressions file is empty");
        }

        foreach (var item in lines)
        {
            var line = item.Split('|');

            yield return new object[] { line[0], decimal.Parse(line[1].AsSpan()) };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
