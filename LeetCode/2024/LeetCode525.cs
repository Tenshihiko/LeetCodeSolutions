using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode525
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(FindMaxLength([0, 1, 0]));
        }

        public int FindMaxLength(int[] nums)
        {

            var prevzeros = new int[nums.Length];
            var prevones = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                prevzeros[i] = (nums[i] == 0 ? 1 : 0)
                        + (i == 0 ? 0 : prevzeros[i - 1]);
                prevones[i] = (nums[i] == 1 ? 1 : 0)
                        + (i == 0 ? 0 : prevones[i - 1]);
            }

            var max = 0;

            for (int i = -1; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    var zeros = prevzeros[j] - (i == -1 ? 0 : prevzeros[i]);
                    var ones = prevones[j] - (i == -1 ? 0 : prevones[i]);

                    if (zeros == ones)
                    {
                        max = Math.Max(max, j - i);
                    }
                }
            }

            return max;
        }
    }
}
