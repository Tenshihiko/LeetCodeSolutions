namespace LeetCode2300;

public class Solution
{
    public void Run()
    {
        // var param = ...;
        var result = SuccessfulPairs([3, 1, 2], [8, 5, 8], 16);
    }

    public int[] SuccessfulPairs(int[] spells, int[] potions, long success)
    {
        var result = new int[spells.Length];

        Array.Sort(potions);

        for (int i = 0; i < spells.Length; i++)
        {
            var firstIndex = BFS(potions, (success + spells[i] - 1) / spells[i]);

            result[i] = Math.Max(0, potions.Length - firstIndex);
        }

        return result;
    }

    private int BFS(int[] potions, long v)
    {
        int left = 0;
        int right = potions.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (potions[mid] * 1L >= v)
            {
                right = mid - 1;
            }
            else
            {
                left = mid + 1;
            }
        }
        return left;
    }
}

