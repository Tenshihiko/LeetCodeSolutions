namespace LeetCode3027;

public class Solution
{
    public void Run()
    {
        int[][] points = [[6, 2], [4, 4], [2, 6]];

        var result = NumberOfPairs(points);
    }

    private int N = 0;

    public int NumberOfPairs(int[][] points)
    {
        int ans = 0;
        N = points.Length;
        var orderedPoints = points.OrderBy(p => p[0]).ThenByDescending(p => p[1]).ToArray();

        for(int i = 0; i < N; i++)
        {
            int[] pointA = orderedPoints[i];
            int xMin = pointA[0] - 1;
            int xMax = int.MaxValue;
            int yMin = int.MinValue;
            int yMax = pointA[1] + 1;

            for (int j = i + 1; j < N; j++)
            {
                int[] pointB = orderedPoints[j];
                if (pointB[0] > xMin && pointB[0] < xMax && pointB[1] > yMin &&
                    pointB[1] < yMax)
                {
                    ans++;
                    xMin = pointB[0];
                    yMin = pointB[1];
                }
            }
        }

        return ans;
    }
}

