using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode368
{
    public class Solution
    {
        public void Run()
        {
            var list = LargestDivisibleSubset(new int[] { 3, 8, 4, 16 });
        }

        private List<int> dfs(int[] nums, int index, bool[] dp, int max)
        {
            if (index >= nums.Length)
            {                
                return new List<int>();
            }

            dp[index] = false;
            var excluded = dfs(nums, index + 1, dp, max);
            List<int> included = new List<int>();
            if (nums[index] % max == 0)
            {
                dp[index] = true;
                included = dfs(nums, index + 1, dp, nums[index]);
                included.Add(nums[index]);
            }

            
            return excluded.Count > included.Count ? excluded : included;
        }

        public IList<int> LargestDivisibleSubset(int[] nums)
        {
            //Array.Sort(nums);

            //var dp = new bool[nums.Length];


            Array.Sort(nums);
            int n = nums.Length;
            int[] dp = new int[n];
            int[] parent = new int[n];
            int maxIndex = 0, maxSize = 0;

            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
                parent[i] = -1;

                for (int j = i - 1; j >= 0; j--)
                {
                    if (nums[i] % nums[j] == 0 && dp[j] + 1 > dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        parent[i] = j;
                    }
                }

                if (dp[i] > maxSize)
                {
                    maxSize = dp[i];
                    maxIndex = i;
                }
            }

            List<int> result = new List<int>();
            while (maxIndex != -1)
            {
                result.Add(nums[maxIndex]);
                maxIndex = parent[maxIndex];
            }

            return result;
        }
    }
}
