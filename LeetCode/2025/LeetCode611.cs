namespace LeetCode611;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        // var result = YourMethod(param);
    }

    public int TriangleNumber(int[] nums)
    {
        Array.Sort(nums);
        var count = 0;

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                var maxC = nums[i] + nums[j] - 1;

                var index = BinarySearchFirstMoreThan(nums, maxC);

                count += index;
            }
        }

        return count / 3;
    }

    private int BinarySearchFirstMoreThan(int[] nums, int maxC)
    {
        var left = 0;
        var right = nums.Length - 1;
        while (left <= right)
        {
            var mid = left + (right - left) / 2;
            if (nums[mid] <= maxC)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return left;
    }

    private bool IsGoodTriangle(int a, int b, int c)
    {
        return a + b > c
            && a + c > b
            && c + b > a;
    }
}

