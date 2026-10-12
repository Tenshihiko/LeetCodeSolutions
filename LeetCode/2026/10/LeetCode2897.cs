namespace LeetCode2897;

public class Solution
{
    public void Run()
    {
        var nums = new List<int> { 3, 5, 6 };
        var k = 3;
        var result = MaxSum(nums, k);
        Console.WriteLine(result); // Output: 98
    }

    public int MaxSum(IList<int> nums, int k)
    {
        var bits = new int[30];
        var n = nums.Count();
        var sum = 0;
        var mod = 1000000007;

        for (int i = 0; i < n; i++)
        {
            CountBits(nums[i], ref bits);
        }

        for (int i = 0; i < k; i++)
        {
            var num = BuildMaxNum(ref bits);

            var sqr = (int)(((long)num * num) % mod);
            sum = (sum + sqr) % mod;
        }

        return sum;
    }

    private void CountBits(int x, ref int[] bits)
    {
        int i = 0;
        while (x > 0)
        {
            bits[i] += x % 2;
            x /= 2;
            i++;
        }
    }

    private int BuildMaxNum(ref int[] bits)
    {
        var num = 0;
        for (int i = bits.Length - 1; i >= 0; i--)
        {
            num = num * 2 + (bits[i] > 0 ? 1 : 0);
            bits[i] = Math.Max(0, bits[i] - 1);
        }

        return num;
    }
}

