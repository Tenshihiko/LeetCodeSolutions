using LeetCode.Common;
using Xunit;

namespace LeetCode2197;

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
    public int[] ReplaceNonCoprimes(int[] nums)
    {
        var stack = new Stack<int>();
        foreach (var num in nums)
        {
            stack.Push(num);
            while (stack.Count > 1)
            {
                var a = stack.Pop();
                var b = stack.Pop();
                var gcd = GCD(a, b);
                if (gcd == 1)
                {
                    stack.Push(b);
                    stack.Push(a);
                    break;
                }
                else
                {
                    var lcm = a / gcd * b;
                    stack.Push(lcm);
                }
            }
        }
        return stack.Reverse().ToArray();
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}
