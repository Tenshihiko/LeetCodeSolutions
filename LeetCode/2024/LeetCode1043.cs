using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1043
{
    internal class Solution
    {
        
        public void Run()
        {
            Console.WriteLine(MaxSumAfterPartitioning(new int[] { 1, 15, 7, 9, 2, 5, 10 }, 3));
        }

        private int MaxSum(int[] arr, int index, int k, int[] dp)
        {

            if (index == arr.Length)
            {
                return 0;
            }

            if (dp[index] != -1)
            {
                return dp[index];
            }

            int end = Math.Min(index + k, arr.Length);
            int len = 0;
            int maxEl = 0;
            int maxSum = 0;

            for (int i = index; i < end; i++)
            {
                len++;
                maxEl = Math.Max(maxEl, arr[i]);
                int sum = len * maxEl + MaxSum(arr, i + 1, k, dp);
                maxSum = Math.Max(maxSum, sum);
            }

            dp[index] = maxSum;
            return maxSum;
        }

        public int MaxSumAfterPartitioning(int[] arr, int k)
        {
            var dp = new int[arr.Length];

            Array.Fill(dp, -1);

            return MaxSum(arr, 0, k, dp);
        }

    }
}
