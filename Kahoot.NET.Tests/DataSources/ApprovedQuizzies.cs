using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Kahoot.NET.Tests.DataSources;

public class ApprovedQuizzies : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var urls = TestHelper.Configuration.GetSection("quizzies").Get<string[]>();

        foreach (var item in urls)
        {
            yield return new object[] { item };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
