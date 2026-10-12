using LeetCode.Common;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode1642;

[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        solution.FurthestBuilding(new int[] { 1, 2 }, 0, 0);
    }
}

public class Solution
{
    public int FurthestBuilding(int[] heights, int bricks, int ladders)
    {
        var largest = new PriorityQueue<int, int>();
        var i = 1;
        for (; i < heights.Length; i++)
        {
            var jump = heights[i] - heights[i - 1];

            if(jump <= 0)
            {
                continue;
            }

            if (largest.Count < ladders)
            {
                largest.Enqueue(jump, jump);
            }
            else
            {
                var smallest = largest.Count > 0 ? largest.Peek() : 0;
                if (jump > smallest && smallest > 0)
                {
                    if (bricks < smallest)
                    {
                        break;
                    }

                    bricks -= smallest;

                    if (largest.Count > 0)
                    {
                        largest.Dequeue();
                    }
                    largest.Enqueue(jump, jump);
                }
                else
                {
                    if (bricks < jump)
                    {
                        break;
                    }

                    bricks -= jump;
                }
            }
        }
        return i - 1;
    }
}
