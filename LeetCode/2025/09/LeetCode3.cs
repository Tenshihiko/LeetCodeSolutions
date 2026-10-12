using LeetCode.Common;
using Xunit;

namespace LeetCode3;

[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        var param = "dvdf";
        var result = solution.LengthOfLongestSubstring(param);
    }
}

public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        var res = 0;
        var used = new Dictionary<char, bool>();

        var start = 0;
        var end = 0;

        do
        {
            var isUsed = used.GetValueOrDefault(s[end], false);

            if (!isUsed)
            {
                used[s[end]] = true;
                res = Math.Max(res, end - start + 1);
                end++;
            }
            else
            {
                do
                {
                    used[s[start]] = false;
                    start++;
                } while (start < end && used[s[end]]);
            }

        } while (end < s.Length);
        return res;
    }
}
