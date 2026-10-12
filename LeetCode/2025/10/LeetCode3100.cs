using LeetCode.Common;
using Xunit;

namespace LeetCode3100;

[Folder]
public class Tests
{
    [Fact]
    public void Examples()
    {
        var solution = new Solution();
        // var param = ...;
        var result = solution.MaxBottlesDrunk(10, 6);
    }
}

public class Solution
{
    public int MaxBottlesDrunk(int numBottles, int numExchange)
    {
        var drunk = 0;
        var full = numBottles;
        var empty = 0;

        while (full > 0)
        {

            drunk += full;
            empty += full;
            full = 0;

            if (empty < numExchange)
            {
                break;
            }

            while (empty >= numExchange)
            {
                empty -= numExchange;
                numExchange++;
                full++;
            }
        }

        return drunk;
    }
}
