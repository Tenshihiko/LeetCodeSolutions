using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode36
{
    public class Solution
    {
        public void Run()
        {
            char[][] board = 
[['5','3','.','.','7','.','.','.','.']
,['6','.','.','1','9','5','.','.','.']
,['.','9','8','.','.','.','.','6','.']
,['8','.','.','.','6','.','.','.','3']
,['4','.','.','8','.','3','.','.','1']
,['7','.','.','.','2','.','.','.','6']
,['.','6','.','.','.','.','2','8','.']
,['.','.','.','4','1','9','.','.','5']
,['.','.','.','.','8','.','.','7','9']];

            var result = IsValidSudoku(board);
        }

        public bool IsValidSudoku(char[][] board)
        {
            var used = new bool[9, 3, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var x = board[i][j] - '1';
                    var b = i / 3 * 3 + j / 3;

                    if (x < 0)
                        continue;

                    if (used[x, 0, i]
                        || used[x, 1, j]
                        || used[x, 2, b])
                        return false;

                    used[x, 0, i] =
                    used[x, 1, j] =
                    used[x, 2, b] = true;
                }
            }

            return true;
        }
    }
}
