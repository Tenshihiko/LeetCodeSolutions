namespace LeetCode3446
{
    public class Solution
    {
        public void Run()
        {
            int[][] grid = [[1, 7, 3], [9, 8, 2], [4, 5, 6]];
            var r = SortMatrix(grid);
        }

        private int N = 0;

        public int[][] SortMatrix(int[][] grid)
        {
            N = grid.Length;
            var giags = 2 * N - 1;

            for (int d = -N + 1; d < N; d++)
            {
                SortDiagonal(grid, d <= 0 ? -d : 0, d <= 0 ? 0 : d, d > 0);
            }

            return grid;
        }

        private void SortDiagonal(int[][] grid, int startX, int startY, bool increase)
        {
            var length = N - (startX + startY);

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    var f = grid[startX + i][startY + i];
                    var s = grid[startX + j][startY + j];

                    if (increase ? f > s : f < s)
                    {
                        var t = grid[startX + i][startY + i];
                        grid[startX + i][startY + i] = grid[startX + j][startY + j];
                        grid[startX + j][startY + j] = t;
                    }
                }
            }
        }
    }
}
