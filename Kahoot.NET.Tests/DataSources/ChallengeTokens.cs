using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Kahoot.NET.Tests.DataSources;

public class ChallengeTokens : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var item in TestHelper.Configuration.GetValue<SuccessfulDecode[]>("successfulJoins"))
        {
            yield return new object[] { item.Header, item.Challenge, item.Expected };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
