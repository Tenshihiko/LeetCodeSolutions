namespace LeetCode3349;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        // var result = YourMethod(param);

        HasIncreasingSubarrays([-15, 19], 1);
    }

    public bool HasIncreasingSubarrays(IList<int> nums, int k)
    {
        var n = nums.Count();
        for (int i = 0; i < n - 2 * k; i++)
        {
            var satisfied = true;
            for (int j = 1; j < k; j++)
            {
                if (nums[i + j] <= nums[i + j - 1]
                || nums[i + j + k] <= nums[i + j + k - 1])
                {
                    satisfied = false;
                    break;
                }
            }
            if (satisfied) return true;
        }

        return false;
    }
}

