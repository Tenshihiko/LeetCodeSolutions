---
name: initproblem
description: Initializes a new LeetCode problem solution file. Use when the user invokes /initproblem <number> or asks to start/init a LeetCode problem by its number.
---

# initproblem

Input: LeetCode problem number (e.g. `/initproblem 2897`).

If the user asks for the Problem of the Day (`/initpotd`, "задача дня", "POTD") without a number, first get it with `pwsh -NoProfile -File .github/skills/initproblem/get-potd.ps1` (the last output line is the number).

## Steps

Run the script from the solution root (pwsh):

```
pwsh -NoProfile -File .github/skills/initproblem/init-problem.ps1 -ProblemNumber <number>
```

The script:
1. Takes the current year and month (`yyyy`, `MM`) and creates `LeetCode/<yyyy>/<MM>` if missing.
2. Copies `LeetCode/LeetCodeTemplate.cs` to `LeetCode/<yyyy>/<MM>/LeetCode<number>.cs`.
3. Replaces `namespace LeetCodeTemplate;` with `namespace LeetCode<number>;` in the new file. `Tests` is marked with `[Folder]`

It fails if `LeetCode<number>.cs` already exists anywhere in the project.

The script also fetches problem data from the public LeetCode GraphQL API (`https://leetcode.com/graphql`):
4. Inserts the C# method signature (from the `csharp` code snippet) into the `Solution` class with body `throw new NotImplementedException();`.
5. Prints a `=== PROBLEM DATA ===` JSON block with `csharpSnippet`, `metaData` (method name, param names/types, return type), `exampleTestcases` (raw inputs, one parameter per line, grouped by params count) and `expectedOutputs`.

If fetching fails, the script only warns; the file is still created from the template.

## Fill the Tests class

Using the printed PROBLEM DATA, replace the placeholder comments in `Tests` of the created file with an xUnit `[Theory]` that has one `[InlineData]` per example, for example:

```csharp
using LeetCode.Common;
using Xunit;

namespace LeetCode1;

[Folder]
public class Tests
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
    public void TwoSum(int[] nums, int target, int[] expected) =>
        Assert.Equal(expected, new Solution().TwoSum(nums, target));
}
```

Rules:
- Split `exampleTestcases` lines into groups of `metaData.params.Length`; the N-th group matches `expectedOutputs[N]`.
- Name the test method after the solution method; parameters are the solution's parameters (same names/types) plus `expected` of the return type.
- Convert LeetCode literals to C# attribute arguments: `[1,2]` -> `new[] { 1, 2 }`, `["a","b"]` -> `new[] { "a", "b" }`, `"abc"` stays a string, `'a'` stays a char, `true/false` unchanged.
- Attribute arguments must be constants, so `[InlineData]` cannot hold jagged arrays (`int[][]`), `IList<...>`, `ListNode`, `TreeNode`, `long`/`double` mixes etc. In that case use `[MemberData(nameof(Cases))]` with `public static IEnumerable<object[]> Cases => new[] { new object[] { ... }, ... };` and build values there (collection expressions are fine inside, e.g. `new int[][] { [1, 2], [3] }`; use helpers from `LeetCode/Common` for `ListNode`/`TreeNode`).
- For `double` results use `Assert.Equal(expected, actual, 5)`.
- If the problem accepts any order of answer, sort the result before asserting and mention it in a comment.
- If the class uses a design-style problem (`metaData` has `classname`/`methods`), write a `[Fact]` per example with the sequence of calls and asserts done manually.
- For results that are hard to express in `Assert.Equal` (jagged arrays, `IList<IList<int>>`, `ListNode`), use `Expect.Equal(expected, actual)` from `LeetCode/Common/Expect.cs`: it compares LeetCode-like text representations, so collection types may differ. If the correct answer is unknown, pass `Expect.Any` as expected (any result is accepted).

After running, build the `LeetCode` project, run the created tests and report the created file path to the user.
