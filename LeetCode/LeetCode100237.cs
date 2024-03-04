using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100237
{
    public class Solution
    {

        public void Run()
        {
            Console.WriteLine(CountSubmatrices(
                new int[][] { new int[] { 7, 6, 3 }, 
                              new int[] { 6, 6, 1 } },
                18));
        }

        public int CountSubmatrices(int[][] grid, int k)
        {
            var n = grid.Length;
            var m = grid[0].Length;

            var sums = new int[n, m];
            var count = 0;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    var a = i == 0 ? 0 : sums[i - 1, j];
                    var b = j == 0 ? 0 : sums[i, j - 1];
                    var c = i == 0 || j == 0 ? 0 : sums[i - 1, j - 1];

                    sums[i, j] = a + b - c + grid[i][j];

                    if (sums[i, j] <= k)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
