namespace LeetCode3495;

public class Solution
{
    public void Run()
    {
        int[][] param = [[1, 2],[2, 4]];
        var result = MinOperations(param);
    }

    public long MinOperations(int[][] queries)
    {
        var res = 0L;

        for (int i = 0; i < queries.Length; i++)
        {
            res += MinOperations(queries[i][0], queries[i][1]);
        }

        return res;
    }

    private long MinOperations(int a, int b)
    {
        var prev = 1;
        var cur = 4;
        var d = 1;

        var ops = 0L;

        while (true)
        {
            var l = Math.Max(a, prev);
            var r = Math.Min(b, cur - 1);

            if (l <= r)
            {
                long intersection = r - l + 1;
                ops += intersection * d;
            }
            else
            {
                if (prev > b)
                {
                    break;
                }
            }

            prev = cur;
            cur <<= 2;
            d++;
        }

        return (ops + 1) / 2;
    }
}

