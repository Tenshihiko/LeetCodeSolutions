using LeetCode.Common;
using Xunit;

namespace LeetCode15;

// TODO optimize
[Folder]
public class Tests
{
    // [Theory]
    // [InlineData(param1, param2, expected)]
    // public void Method(int param1, int param2, int expected) =>
    //     Expect.Equal(expected, new Solution().Method(param1, param2));
}

public class Solution
{
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var count = new Dictionary<int, int>();

        var result = new HashSet<(int x, int y, int z)>(new TupleEqualityComparer());

        for (int i = 0; i < nums.Length; i++)
        {
            count[nums[i]] = count.GetValueOrDefault(nums[i], 0) + 1;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {                
                var target = -(nums[i] + nums[j]);

                if (count.ContainsKey(target))
                {
                    if (target == nums[i] && target == nums[j])
                    {
                        if (count[target] >= 3)
                        {
                            result.Add((nums[i], nums[j], target));
                        }
                    }
                    else if (target == nums[i] || target == nums[j])
                    {
                        if (count[target] >= 2)
                        {
                            result.Add((nums[i], nums[j], target));
                        }
                    }
                    else
                    {
                        result.Add((nums[i], nums[j], target));
                    }
                }
            }
        }

        return [.. result.Select(x => new List<int> { x.x, x.y, x.z })];
    }

    private class TupleEqualityComparer : IEqualityComparer<(int x, int y, int z)>
    {
        public bool Equals((int x, int y, int z) a, (int x, int y, int z) b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z
                || a.x == b.x && a.y == b.z && a.z == b.y
                || a.x == b.y && a.y == b.x && a.z == b.z
                || a.x == b.y && a.y == b.z && a.z == b.x
                || a.x == b.z && a.y == b.x && a.z == b.y
                || a.x == b.z && a.y == b.y && a.z == b.x;
        }

        public int GetHashCode((int x, int y, int z) obj)
        {
            return obj.x + obj.y + obj.z;
        }
    }
}
