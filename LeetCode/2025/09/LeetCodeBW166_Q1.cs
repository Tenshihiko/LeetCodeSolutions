using LeetCode.Common;
using Xunit;

namespace LeetCodeBW166_Q1;

[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        int[] param = [1,2,3,4];
        var result = solution.ClimbStairs(param.Length, param);
    }
}

public class Solution
{
    public int ClimbStairs(int n, int[] costs)
    {
        Recursion(costs, 0, 0);
        return MinCost;
    }
    public int MinCost = int.MaxValue;
    private void Recursion(int[] costs, int index, int currentCost)
    {
        if (index > costs.Length)
        {
            return;
        }
        if (index == costs.Length)
        {
            MinCost = Math.Min(MinCost, currentCost);
            return;
        }

        Recursion(costs, index + 1, currentCost + (index + 1 >= costs.Length ? 0 : costs[index + 1]) + 1);
        Recursion(costs, index + 2, currentCost + (index + 2 >= costs.Length ? 0 : costs[index + 2]) + 4);
        Recursion(costs, index + 3, currentCost + (index + 3 >= costs.Length ? 0 : costs[index + 3]) + 9);
    }

    public string MajorityFrequencyGroup(string s)
    {
        var freq = new Dictionary<char, int>();

        foreach (var c in s)
        {
            if (!freq.ContainsKey(c))
            {
                freq[c] = 0;
            }
            freq[c]++;
        }

        var grouped = freq.GroupBy(x => x.Value).OrderByDescending(x => x.Key);
        var maxGroup = grouped.Max(x => x.Count());

        var result = grouped.First(x => x.Count() == maxGroup).Select(grouped => grouped.Key);
        return new string(result.ToArray());
    }
}
