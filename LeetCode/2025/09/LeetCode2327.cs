using LeetCode.Common;
using Xunit;

namespace LeetCode2327;

[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        // var param = ...
        var result = solution.PeopleAwareOfSecret(1000, 1, 1000);
    }
}

public class Solution
{
    public int PeopleAwareOfSecret(int n, int delay, int forget)
    {
        var dp = new int[n + 1, forget];

        dp[1, 0] = 1;
        for (int i = 2; i <= n; i++)
        {
            for (int j = delay - 1; j < forget - 1; j++)
            {
                dp[i, 0] = (dp[i, 0] + dp[i - 1, j]) % 1000000007;
            }

            for (int j = 1; j < forget; j++)
            {
                dp[i, j] = dp[i - 1, j - 1];
            }
        }

        var result = 0;
        for (int j = 0; j < forget; j++)
        {
            result = (result + dp[n, j]) % 1000000007;
        }

        return result;
    }
}
