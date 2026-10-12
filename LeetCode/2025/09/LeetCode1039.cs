using LeetCode.Common;
using Xunit;

namespace LeetCode1039;
[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        // var param = ...;
        var result = solution.MinScoreTriangulation([1, 3, 1, 4, 1, 5]);
    }
}

public class Solution
{
    public int MinScoreTriangulation(int[] values)
    {
        int n = values.Length;
        int[,] dp = new int[n, n];
        for (int length = 2; length < n; length++)
        {
            for (int i = 0; i + length < n; i++)
            {
                if(length == 2)
                {
                    dp[i, i + length] = values[i] * values[i + 1] * values[i + 2];
                    continue;
                }

                dp[i, i + length] = int.MaxValue;
                for (int k = i + 1; k < i + length; k++)
                {
                    dp[i, i + length] = Math.Min(dp[i, i + length], dp[i, k] + dp[k, i + length] + values[i] * values[k] * values[i + length]);
                }
            }
        }
        return dp[0, n - 1];
    }
}
