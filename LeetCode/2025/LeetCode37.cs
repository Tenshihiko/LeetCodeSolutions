using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode37;

public class Solution
{
    public void SolveSudoku(char[][] board)
    {
        var imposible = new bool[9, 9];
        var result = new int[9, 9];
        var unflledCellsCount = 81;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                var x = board[i][j] - '1';
                var b = i / 3 * 3 + j / 3;

                if (x < 0)
                    continue;

                unflledCellsCount--;

                ClearRow(board, imposible, x, j);
                ClearColumn(board, imposible, x, i);
                ClearBox(board, imposible, x, b);
            }
        }
    }

    private void ClearBox(char[][] board, bool[,] imposible, int x, int b)
    {
        throw new NotImplementedException();
    }

    private void ClearColumn(char[][] board, bool[,] imposible, int x, int i)
    {
        throw new NotImplementedException();
    }

    private void ClearRow(char[][] board, bool[,] imposible, int x, int j)
    {
        throw new NotImplementedException();
    }
}
