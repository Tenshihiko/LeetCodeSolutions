namespace LeetCode3542;

public class Solution
{
    public void Run()
    {
        var result = MinOperations([4,4]);

        Console.WriteLine(result);
    }
    public int MinOperations(int[] nums)
    {
        var stack = new Stack<int>();

        var opsCount = 0;

        foreach (var num in nums)
        {
            if (num == 0)
            {
                opsCount += stack.Count;
                stack.Clear();
            } else if (stack.Count == 0)
            {
                stack.Push(num);
            }
            else
            {
                var peek = stack.Peek();

                if (peek == num)
                {
                    continue;
                }
                else if (peek > num)
                {
                    do
                    {
                        if (peek > num) opsCount++;
                        stack.Pop();
                        if (stack.Count > 0) peek = stack.Peek();
                    } while (peek >= num && stack.Count > 0);
                    stack.Push(num);
                }
                else
                {
                    stack.Push(num);
                }
            }
        }

        return opsCount + stack.Count;
    }
}

