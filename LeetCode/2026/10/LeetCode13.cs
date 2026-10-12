using LeetCode.Common;
using Xunit;

namespace LeetCode13;

[Folder]
public class Tests
{
    [Theory]
    [InlineData("III", 3)]
    [InlineData("LVIII", 58)]
    [InlineData("MCMXCIV", 1994)]
    public void RomanToInt(string s, int expected) =>
        Assert.Equal(expected, new Solution().RomanToInt(s));
}

public class Solution
{
    private static Dictionary<char, int> Roman = new Dictionary<char, int>()
    {
        ['I'] = 1,
        ['V'] = 5,
        ['X'] = 10,
        ['L'] = 50,
        ['C'] = 100,
        ['D'] = 500,
        ['M'] = 1000,
    };

    public int RomanToInt(string s)
    {
        var res = 0;
        var max = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            var val = Roman[s[i]];

            res = res + (val >= max ? val : -val);
            max = Math.Max(max, val);
        }

        return res;
    }
}

