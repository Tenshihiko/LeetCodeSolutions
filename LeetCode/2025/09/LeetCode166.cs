using LeetCode.Common;
using Xunit;
using System.Text;

namespace LeetCode166;

[Folder]
public class Tests
{
    // [Theory]
    // [InlineData(param1, param2, expected)]
    // public void Method(int param1, int param2, int expected) =>
    //     Expect.Equal(expected, new Solution().Method(param1, param2));
}

public class Solution
{
    public string FractionToDecimal(int numerator, int denominator)
    {
        if (numerator == 0) return "0";
        var sb = new StringBuilder();
        if ((numerator < 0) ^ (denominator < 0)) sb.Append('-');
        long num = Math.Abs((long)numerator);
        long den = Math.Abs((long)denominator);
        sb.Append(num / den);
        num %= den;
        if (num == 0) return sb.ToString();
        sb.Append('.');
        var map = new Dictionary<long, int>();
        while (num != 0)
        {
            if (map.ContainsKey(num))
            {
                sb.Insert(map[num], '(');
                sb.Append(')');
                break;
            }
            map[num] = sb.Length;
            num *= 10;
            sb.Append(num / den);
            num %= den;
        }
        return sb.ToString();
    }
}
