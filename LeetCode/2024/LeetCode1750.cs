using LeetCode.Common;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1750;

[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        Expect.Check(solution.MinimumLength("bbbbbbbbbbbbbbbbbbb"));
    }
}

public class Solution
{
    public int MinimumLength(string s)
    {
        var left = 0;
        var right = s.Length - 1;

        while (left < right && s[left] == s[right])
        {
            while (left < right && s[left] == s[left + 1])
            {
                left++;
            }

            while (left < right && s[right] == s[right - 1])
            {
                right--;
            }

            left++;
            right--;
        }

        return Math.Max(0, right - left + 1);
    }
}
