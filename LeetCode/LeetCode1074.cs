using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1074
{
    public class Solution
    {
        public int NumSubmatrixSumTarget(int[][] matrix, int target)
        {
            var n = matrix.Length;
            var m = matrix[0].Length;
            var sum = new int[n + 1, m + 1];
            var count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int ulx = 1; ulx <= i + 1; ulx++)
                {
                    var matrix_sums = new Dictionary<int, int>();
                    matrix_sums[0] = 1;
                    for (int j = 0; j < m; j++)
                    {
                        sum[i + 1, j + 1] = sum[i + 1, j]
                                            + sum[i, j + 1]
                                            - sum[i, j]
                                            + matrix[i][j];

                        var current = sum[i + 1, j + 1] - sum[ulx - 1, j + 1];
                        count += matrix_sums.GetValueOrDefault(current - target, 0);
                        matrix_sums[current] = matrix_sums.GetValueOrDefault(current, 0) + 1;
                    }
                }
            }

            return count;
        }

        public void Run()
        {
            var solution = new Solution();

            int result = solution.NumSubmatrixSumTarget(new int[][] {
    new int[]{ 904, 1, 0 },
    new int[]{ 1, 1, 1 },
    new int[]{ 0, 1, 0 } }, 0);

            Console.WriteLine(result);
        }
    }
}
