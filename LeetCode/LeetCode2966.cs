using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode2966
{
    public class Solution
    {
        public void Run()
        {
            var arr = new int[] { 1, 3, 3, 2, 7, 3 };

            var res = DivideArray(arr, 3);

            
        }

        public int[][] DivideArray(int[] nums, int k)
        {
            var c = 3;
            var n = nums.Length;
            var m = n / c;
            var result = new int[c][];

            Array.Sort(nums);

            for (int i = 0; i < c; i ++)
            {
                result[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    result[i][j] = nums[i * m + j];
                }

                if (result[i][m - 1] - result[i][0] > k)
                {
                    result = new int[0][];
                    break;
                }
            }

            return result;
        }
    }
}
