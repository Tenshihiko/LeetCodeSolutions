namespace LeetCode1792;

public class Solution
{
    public void Run()
    {
        int[][] classes = [[2, 4], [3, 9], [4, 5], [2, 10]];

        var result = MaxAverageRatio(classes, 4);
    }

    public double MaxAverageRatio(int[][] classes, int extraStudents)
    {
        var queue = new PriorityQueue<(int pass, int total, double deltaPassRate), double>();


        var preparedClasses = classes.Select(c => (
            pass: c[0],
            total: c[1],
            deltaPassRate: (double)c[0] / c[1] - (double)(c[0] + 1) / (c[1] + 1)));

        foreach (var c in preparedClasses)
        {
            queue.Enqueue((c.pass, c.total, c.deltaPassRate), c.deltaPassRate);
        }

        while (extraStudents > 0)
        {
            (int pass, int total, double deltaPassRate) = queue.Dequeue();
            pass++;
            total++;
            deltaPassRate = (double)pass / total - (double)(pass + 1) / (total + 1);
            queue.Enqueue((pass, total, deltaPassRate), deltaPassRate);
            extraStudents--;
        }

        double totalPassRate = 0;
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            totalPassRate += (double)c.pass / c.total;
        }

        return totalPassRate / classes.Length;
    }
}

