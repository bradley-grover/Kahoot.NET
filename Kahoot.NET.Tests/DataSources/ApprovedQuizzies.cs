using System.Collections;
using System.Collections.Generic;

namespace Kahoot.NET.Tests.DataSources;

public class ApprovedQuizzies : IEnumerable<object[]>
{
    public static readonly string[] Urls = new string[]
    {
        "https://create.kahoot.it/details/faf45afa-050b-440e-881b-4845801df788",
        "https://create.kahoot.it/details/cec5cac3-5a2d-44bf-abcf-5d9f733a427a"
    };

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var item in Urls)
        {
            yield return new object[] { item };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
