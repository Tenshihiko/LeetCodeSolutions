namespace LeetCode3469
{

    public class Solution
    {
        public void Run()
        {
            var res = LenOfVDiagonal([[2, 2, 1, 2, 2], 
                                      [2, 0, 2, 2, 0], 
                                      [2, 0, 1, 1, 0], 
                                      [1, 0, 2, 2, 2], 
                                      [2, 0, 0, 2, 2]]);
        }
        private int N = 0;
        private int M = 0;

        public int LenOfVDiagonal(int[][] grid)
        {
            var longestLength = 0;
            N = grid.Length;
            M = grid[0].Length;

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        var NW = LongestVDiagonal(grid, i, j, 1, 1, dx: -1, dy: -1, turned: false);
                        var NE = LongestVDiagonal(grid, i, j, 1, 1, dx: 1, dy: -1, turned: false);
                        var SW = LongestVDiagonal(grid, i, j, 1, 1, dx: -1, dy: 1, turned: false);
                        var SE = LongestVDiagonal(grid, i, j, 1, 1, dx: 1, dy: 1, turned: false);

                        longestLength = Max(longestLength, NW, NE, SW, SE);
                    }
                }
            }

            return longestLength;
        }

        private int LongestVDiagonal(int[][] grid, int i, int j, int length, int prev, int dx, int dy, bool turned)
        {
            var x = i + dx;
            var y = j + dy;
            if (x < 0 || x >= N || y < 0 || y >= M || grid[x][y] == 1)
                return length;

            if (!(prev < 2 && grid[x][y] == 2
                || prev == 2 && grid[x][y] == 0))
                return length;

            var longestLength = LongestVDiagonal(grid, x, y, length + 1, grid[x][y], dx, dy, turned);

            if (!turned)
            {
                var length2 = LongestVDiagonal(grid, x, y, length + 1, grid[x][y], dy, -dx, true);

                longestLength = Math.Max(longestLength, length2);
            }

            return longestLength;
        }

        public static int Max(params int[] values)
        {
            return Enumerable.Max(values);
        }
    }
}
