using LeetCode.Common;
using Xunit;

namespace LeetCode17;

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
    Dictionary<int, string> Letters = new Dictionary<int, string>()
    {
        [2] = "abc",
        [3] = "def",
        [4] = "ghi",
        [5] = "jkl",
        [6] = "mno",
        [7] = "pqrs",
        [8] = "tuv",
        [9] = "wxyz",
    };

    public IList<string> LetterCombinations(string digits)
    {
        var result = new List<string>();

        BuildCombinationRecursive(digits, 0, "", result);

        return result;
    }

    private void BuildCombinationRecursive(string digits, int index, string str, List<string> result)
    {
        if (str.Length == digits.Length)
        {
            if (str.Length > 0)
            {
                result.Add(str);
            }
            return;
        }

        var digit = digits[index] - '0';
        var letters = Letters[digit];

        foreach (var letter in letters)
        {
            BuildCombinationRecursive(digits, index + 1, str + letter, result);
        }
    }
}
