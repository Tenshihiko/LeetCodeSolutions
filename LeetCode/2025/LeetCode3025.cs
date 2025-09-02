namespace LeetCode3025;

// TODO optimize with sweepline
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
        N = points.Length;
        var pairs = 0;
        for (int i = 0; i < N; i++)
        {
            for (int j = i + 1; j < N; j++)
            {
                if (!(points[i][0] <= points[j][0] && points[i][1] >= points[j][1]
                 || points[i][0] >= points[j][0] && points[i][1] <= points[j][1]))
                    continue;

                var isValid = CheckPair(points, i, j);

                pairs += isValid ? 1 : 0;
            }
        }

        return pairs;
    }

    private bool CheckPair(int[][] points, int i, int j)
    {
        for (int k = 0; k < N; k++)
        {
            if (k == i || k == j)
                continue;

            var @in = CheckPoints(points[i][0], points[i][1],
                                 points[j][0], points[j][1],
                                 points[k][0], points[k][1]);

            if (@in)
                return false;
        }

        return true;
    }

    private bool CheckPoints(int ix, int iy, int jx, int jy, int kx, int ky)
    {
        if (kx >= Math.Min(ix, jx) && kx <= Math.Max(ix, jx)
            && ky >= Math.Min(iy, jy) && ky <= Math.Max(iy, jy))
            return true;
        return false;
    }
}

