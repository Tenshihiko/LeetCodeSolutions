using LeetCode.Common;
using Xunit;

namespace LeetCode3568;

[Folder]
public class Tests
{
    [Theory]
    [InlineData(new[] { "S.", "XL" }, 2, 2)]
    [InlineData(new[] { "LS", "RL" }, 4, 3)]
    [InlineData(new[] { "L.S", "RXL" }, 3, -1)]
    public void MinMoves(string[] classroom, int energy, int expected) =>
        Assert.Equal(expected, new Solution().MinMoves(classroom, energy));
}

public class Solution
{
    public int MinMoves(string[] classroom, int energy)
    {
        var rows = classroom.Length;
        var cols = classroom[0].Length;
        var litterIndex = new int[rows, cols];
        var litterCount = 0;
        int startRow = 0, startCol = 0;

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < cols; c++)
            {
                litterIndex[r, c] = -1;
                if (classroom[r][c] == 'L') litterIndex[r, c] = litterCount++;
                else if (classroom[r][c] == 'S') (startRow, startCol) = (r, c);
            }
        }

        var fullMask = (1 << litterCount) - 1;
        if (fullMask == 0) return 0;

        // best[r, c, mask] = max remaining energy seen in this state; -1 = not visited
        var best = new int[rows, cols, 1 << litterCount];
        for (var r = 0; r < rows; r++)
            for (var c = 0; c < cols; c++)
                for (var m = 0; m <= fullMask; m++)
                    best[r, c, m] = -1;

        var queue = new Queue<(int Row, int Col, int Mask, int Energy)>();
        queue.Enqueue((startRow, startCol, 0, energy));
        best[startRow, startCol, 0] = energy;

        var directions = new (int Dr, int Dc)[] { (1, 0), (-1, 0), (0, 1), (0, -1) };
        var moves = 0;

        while (queue.Count > 0)
        {
            moves++;
            for (var levelSize = queue.Count; levelSize > 0; levelSize--)
            {
                var (row, col, mask, currentEnergy) = queue.Dequeue();
                if (currentEnergy == 0) continue;

                foreach (var (dr, dc) in directions)
                {
                    var nr = row + dr;
                    var nc = col + dc;
                    if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || classroom[nr][nc] == 'X') continue;

                    var nextEnergy = classroom[nr][nc] == 'R' ? energy : currentEnergy - 1;
                    var nextMask = litterIndex[nr, nc] >= 0 ? mask | (1 << litterIndex[nr, nc]) : mask;

                    if (nextMask == fullMask) return moves;
                    if (nextEnergy <= best[nr, nc, nextMask]) continue;

                    best[nr, nc, nextMask] = nextEnergy;
                    queue.Enqueue((nr, nc, nextMask, nextEnergy));
                }
            }
        }

        return -1;
    }
}

