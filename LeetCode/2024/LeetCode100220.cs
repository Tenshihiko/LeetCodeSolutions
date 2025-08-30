using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100220
{
    public class Solution
    {
        public void Run()
        {
            MaxOperations(new int[] { 2, 5, 3, 4, 5, 2, 6, 3, 7 });
        }
        public int MaxOperations(int[] s)
        {
            var n = s.Length;
            var dp = new int[n, n, 2000];


            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = i; j < n; j++)
                {
                    if (i == j)
                    {

                    }
                    else if (j - i == 1)
                    {
                        dp[i, j, s[i] + s[j]] = 1;
                    }
                    else
                    {
                        dp[i, j, s[i] + s[i + 1]] = Math.Max(dp[i, j, s[i] + s[i + 1]], dp[i + 2, j, s[i] + s[i + 1]] + 1);
                        dp[i, j, s[i] + s[j]] = Math.Max(dp[i, j, s[i] + s[j]], dp[i + 1, j - 1, s[i] + s[j]] + 1);
                        dp[i, j, s[j - 1] + s[j]] = Math.Max(dp[i, j, s[j - 1] + s[j]], dp[i, j - 2, s[j - 1] + s[j]] + 1);
                    }
                }
            }

            var first = dp[0, n - 1, s[0] + s[1]];
            var both = dp[0, n - 1, s[0] + s[n - 1]];
            var last = dp[0, n - 1, s[n - 2] + s[n - 1]];

            return Math.Max(first, Math.Max(both, last));

        }
    }
}
