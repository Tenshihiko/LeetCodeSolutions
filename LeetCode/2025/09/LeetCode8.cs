namespace LeetCode8;

public class Solution
{
    public void Run()
    {
        var param = "-91283472332";
        var result = MyAtoi(param);
    }

    public int MyAtoi(string s)
    {
        if (s == "") return 0;

        long result = 0;

        var i = 0;

        while (i < s.Length && s[i] == ' ') i++;

        if (i == s.Length) return 0;

        var isPositive = s[i] == '+' || s[i] >= '0' && s[i] <= '9';
        var isNegative = s[i] == '-';

        if (!isPositive && !isNegative) return 0;

        if (s[i] == '-' || s[i] == '+') i++;

        while (i < s.Length && s[i] == '0') i++;

        while (i < s.Length && s[i] >= '0' && s[i] <= '9')
        {
            result = result * 10 + s[i] - '0';
            i++;
            if (result > int.MaxValue) break;
        }

        return isPositive ? result > int.MaxValue ? int.MaxValue : (int)result
            : -result < int.MinValue ? int.MinValue : -(int)result;
    }
}

