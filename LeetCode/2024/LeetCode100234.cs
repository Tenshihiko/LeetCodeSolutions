using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode100234
{
    public class Solution
    {

        public void Run()
        {
            Console.WriteLine(MinimumOperationsToWriteY(
                new int[][] { 
                    new int[]{ 1,2,2},
                    new int[]{ 1,1,0},
                    new int[]{ 0,1,0},
                }));
        }
        public int MinimumOperationsToWriteY(int[][] grid)
        {
            var n = grid.Length;
            var n2 = n / 2;
            var y = new int[3];
            var b = new int[3];

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if (i == j && i < n2
                        || i == n - j - 1 && i < n2
                        || j == n2 && i >= n2)
                    {
                        y[grid[i][j]]++;
                    } else
                    {
                        b[grid[i][j]]++;
                    }
                }
            }

            var yCount = n + n2;

            var c01 = y[1] + y[2] + b[0] + b[2];
            var c02 = y[1] + y[2] + b[0] + b[1];
            var c10 = y[0] + y[2] + b[1] + b[2];
            var c12 = y[0] + y[2] + b[0] + b[1];
            var c20 = y[0] + y[1] + b[1] + b[2];
            var c21 = y[0] + y[1] + b[0] + b[2];

            return Math.Min(Math.Min(Math.Min(c01, c02), Math.Min(c10,c12)), Math.Min(c20,c21));
        }
    }
}
