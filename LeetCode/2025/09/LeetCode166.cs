using System.Text;

namespace LeetCode166;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        // var result = YourMethod(param);
    }

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

