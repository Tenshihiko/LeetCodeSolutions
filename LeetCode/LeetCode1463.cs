using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1463
{
    public class Solution
    {
        public void Run()
        {
            CherryPickup(new int[][] { new int[]{ 1, 0, 0, 0, 0, 0, 1},
            new int[]{2, 0, 0, 0, 0, 3, 0 },
            new int[]{2, 0, 9, 0, 0, 0, 0 },
            new int[]{0, 3, 0, 5, 4, 0, 0 },
            new int[]{1, 0, 2, 3, 0, 0, 6 } });
        }

        public int CherryPickup(int[][] grid)
        {
            var n = grid.Length;
            var m = grid[0].Length;

            var dp = new int[n, m, m];

            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 0; j < m; j++)
                {
                    for (int k = m - 1; k >= 0; k--)
                    {
                        var max = 0;
                        if (i < n - 1)
                        {
                            for (var jx = -1; jx <= 1; jx++)
                            {
                                for (var kx = -1; kx <= 1; kx++)
                                {
                                    if (j + jx < 0 || j + jx >= m
                                    || k + kx < 0 || k + kx >= m)
                                    {
                                        continue;
                                    }

                                    max = Math.Max(max, dp[i + 1, j + jx, k + kx]);
                                }
                            }
                        }

                        dp[i, j, k] = grid[i][j] + (j != k ? grid[i][k] : 0) + max;
                    }
                }
            }

            return dp[0, 0, m - 1];
        }
    }
}
