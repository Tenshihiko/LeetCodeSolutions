using System.Text;

namespace LeetCode12;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        // var result = YourMethod(param);
    }

    public string IntToRoman(int num)
    {
        var result = new StringBuilder();

        num = Convert(num, result, 9000, "_", 5000, "_", 4000, "_", 1000, "M");
        num = Convert(num, result, 900, "CM", 500, "D", 400, "CD", 100, "C");
        num = Convert(num, result, 90, "XC", 50, "L", 40, "XL", 10, "X");
        num = Convert(num, result, 9, "IX", 5, "V", 4, "IV", 1, "I");

        return result.ToString();
    }

    private static int Convert(int num, StringBuilder result,
        int nine, string nine_str,
        int five, string five_str,
        int four, string four_str,
        int one, string one_str)
    {
        if (num >= nine)
        {
            result.Append(nine_str);
            num -= nine;
        }
        else if (num >= five)
        {
            result.Append(five_str);
            num -= five;
        }
        else if (num >= four)
        {
            result.Append(four_str);
            num -= four;
        }

        while (num >= one)
        {
            result.Append(one_str);
            num -= one;
        }

        return num;
    }
}

