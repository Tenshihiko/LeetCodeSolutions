namespace LeetCode2962
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(CountSubarrays([1, 3, 2, 3, 3], 2));
        }
        public long CountSubarrays(int[] nums, int k)
        {
            var maxCount = 0;
            var max = 0;
            var n = nums.Length;

            foreach (var num in nums)
            {
                if (num == max)
                {
                    maxCount++;
                }
                else if (num > max)
                {
                    max = num;
                    maxCount = 1;
                }
            }

            if (maxCount < k)
            {
                return 0;
            }

            var maxInWindow = 0;
            var left = -1;
            var right = -1;

            var count = 0;

            while (right < n)
            {
                while (right < n)
                {
                    right++;

                    if (right < n)
                    {
                        if (nums[right] == max)
                        {
                            maxInWindow++;
                        }

                        if (maxInWindow >= k)
                        {
                            break;
                        }
                    }
                }

                var m = right - left - 1;

                count += (1 + m) * m / 2;

                while (right < n && left < right)
                {
                    left++;

                    if (nums[left] == max)
                    {
                        maxInWindow--;
                        break;
                    }
                }
            }

            return (1 + n) * n / 2 - count + 1;
        }
    }
}
