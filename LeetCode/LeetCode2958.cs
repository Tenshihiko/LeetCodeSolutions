using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode2958
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(MaxSubarrayLength([1], 1));
        }

        public int MaxSubarrayLength(int[] nums, int k)
        {
            var frequencies = new Dictionary<int, int>();

            var left = -1;
            var right = -1;

            var longest = 0;

            while (right < nums.Length)
            {
                var extra = -1;
                while (right < nums.Length)
                {
                    right++;

                    if (right < nums.Length)
                    {
                        var f = frequencies.GetValueOrDefault(nums[right], 0);

                        frequencies[nums[right]] = f + 1;

                        if (f + 1 > k)
                        {
                            extra = nums[right];
                            break;
                        }
                    }

                }

                longest = Math.Max(longest, right - left - 1);

                while (right < nums.Length && left < right)
                {
                    left++;
                    frequencies[nums[left]] = frequencies[nums[left]] - 1;

                    if (nums[left] == extra)
                    {
                        break;
                    }
                }
            }

            return longest;
        }
    }
}
