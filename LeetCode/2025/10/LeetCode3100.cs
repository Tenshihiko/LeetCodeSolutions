namespace LeetCode3100;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        var result = MaxBottlesDrunk(10, 6);
    }

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

