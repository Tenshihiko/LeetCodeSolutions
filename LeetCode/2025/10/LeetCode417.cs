
namespace LeetCode417;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        var result = PacificAtlantic([[1, 2, 2, 3, 5], [3, 2, 3, 4, 4], [2, 4, 5, 3, 1], [6, 7, 1, 4, 5], [5, 1, 1, 2, 4]]);
    }

    private int N;
    private int M;

    public IList<IList<int>> PacificAtlantic(int[][] heights)
    {
        N = heights.Length;
        M = heights[0].Length;

        var result = new List<IList<int>>();

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {

                var queue = new Queue<(int x, int y)>();
                queue.Enqueue((i, j));
                var visited = new bool[N, M];
                var reachablePacific = false;
                var reachableAtlantic = false;

                while (queue.Count > 0)
                {
                    var (x, y) = queue.Dequeue();
                    if (visited[x, y])
                    {
                        continue;
                    }
                    visited[x, y] = true;
                    if (x == 0 || y == 0)
                    {
                        reachablePacific = true;
                    }
                    if (x == N - 1 || y == M - 1)
                    {
                        reachableAtlantic = true;
                    }
                    if (reachablePacific == true && reachableAtlantic == true)
                    {
                        break;
                    }
                    var directions = new (int dx, int dy)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
                    foreach (var (dx, dy) in directions)
                    {
                        var newX = x + dx;
                        var newY = y + dy;
                        if (newX >= 0 && newX < N && newY >= 0 && newY < M && !visited[newX, newY] && heights[newX][newY] <= heights[x][y])
                        {
                            queue.Enqueue((newX, newY));
                        }
                    }
                }

                if (reachablePacific && reachableAtlantic)
                {
                    result.Add([i, j]);
                }
            }
        }

        return result;
    }
}

