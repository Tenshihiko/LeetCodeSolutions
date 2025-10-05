namespace LeetCode407;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        //var result = TrapRainWater([[1, 4, 3, 1, 3, 2],
        //                            [3, 2, 1, 3, 2, 4],
        //                            [2, 3, 3, 2, 3, 1]]);

        var result = TrapRainWater([[3, 3, 3, 3, 3], [3, 2, 2, 2, 3], [3, 2, 1, 2, 3], [3, 2, 2, 2, 3], [3, 3, 3, 3, 3]]);
    }

    private int N;
    private int M;

    public int TrapRainWater(int[][] heightMap)
    {
        N = heightMap.Length;
        M = heightMap[0].Length;

        var trappedWater = 0;
        var maxHeight = heightMap.Aggregate(int.MinValue, (max, row) => Math.Max(max, row.Max()));
        var minHeight = heightMap.Aggregate(int.MaxValue, (min, row) => Math.Min(min, row.Min()));

        for (int lvl = minHeight + 1; lvl <= maxHeight; lvl++)
        {
            trappedWater += TrapRainWaterLayer(heightMap, lvl);
        }
        return trappedWater;
    }

    public int TrapRainWaterLayer(int[][] heightMap, int lvl)
    {
        var trappedWater = 0;
        var checkedPoints = new bool[N, M];
        for (int i = 1; i < heightMap.Length - 1; i++)
        {
            for (int j = 1; j < heightMap[0].Length - 1; j++)
            {
                var pond = 0;
                var queue = new Queue<(int x, int y)>();

                if (heightMap[i][j] < lvl && !checkedPoints[i, j])
                {
                    queue.Enqueue((i, j));
                }
                else
                {
                    continue;
                }

                while (queue.Count > 0)
                {
                    var (x, y) = queue.Dequeue();
                    if (checkedPoints[x, y])
                    {
                        continue;
                    }
                    checkedPoints[x, y] = true;

                    if (x == 0 || x == N - 1 || y == 0 || y == M - 1)
                    {
                        pond = 0;

                        MarkAllConnectedAsChecked(x, y, heightMap, lvl, checkedPoints, queue);

                        break;
                    }

                    pond++;

                    if (x > 0 && !checkedPoints[x - 1, y] && heightMap[x - 1][y] < lvl)
                    {
                        queue.Enqueue((x - 1, y));
                    }
                    if (x < N - 1 && !checkedPoints[x + 1, y] && heightMap[x + 1][y] < lvl)
                    {
                        queue.Enqueue((x + 1, y));
                    }
                    if (y > 0 && !checkedPoints[x, y - 1] && heightMap[x][y - 1] < lvl)
                    {
                        queue.Enqueue((x, y - 1));
                    }
                    if (y < M - 1 && !checkedPoints[x, y + 1] && heightMap[x][y + 1] < lvl)
                    {
                        queue.Enqueue((x, y + 1));
                    }
                }

                trappedWater += pond;
            }
        }
        return trappedWater;
    }

    private void MarkAllConnectedAsChecked(int x, int y, int[][] heightMap, int lvl, bool[,] checkedPoints, Queue<(int x, int y)> queue)
    {
        queue.Enqueue((x, y));

        while (queue.Count > 0)
        {
            var (cx, cy) = queue.Dequeue();
            checkedPoints[cx, cy] = true;
            if (cx > 0 && !checkedPoints[cx - 1, cy] && heightMap[cx - 1][cy] < lvl)
            {
                queue.Enqueue((cx - 1, cy));
            }
            if (cx < N - 1 && !checkedPoints[cx + 1, cy] && heightMap[cx + 1][cy] < lvl)
            {
                queue.Enqueue((cx + 1, cy));
            }
            if (cy > 0 && !checkedPoints[cx, cy - 1] && heightMap[cx][cy - 1] < lvl)
            {
                queue.Enqueue((cx, cy - 1));
            }
            if (cy < M - 1 && !checkedPoints[cx, cy + 1] && heightMap[cx][cy + 1] < lvl)
            {
                queue.Enqueue((cx, cy + 1));
            }
        }
    }
}

