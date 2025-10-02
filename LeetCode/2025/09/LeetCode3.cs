namespace LeetCode3;

public class Solution
{
    public void Run()
    {
        var param = "dvdf";
        var result = LengthOfLongestSubstring(param);
    }

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

