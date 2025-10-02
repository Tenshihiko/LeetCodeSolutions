namespace LeetCode2197;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        // var result = YourMethod(param);
    }

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

