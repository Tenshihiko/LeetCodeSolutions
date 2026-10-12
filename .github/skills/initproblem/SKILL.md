---
name: initproblem
description: Initializes a new LeetCode problem solution file. Use when the user invokes /initproblem <number> or asks to start/init a LeetCode problem by its number.
---

# initproblem

Input: LeetCode problem number (e.g. `/initproblem 2897`).

## Steps

Run the script from the solution root (pwsh):

```
pwsh -NoProfile -File .github/skills/initproblem/init-problem.ps1 -ProblemNumber <number>
```

The script:
1. Takes the current year and month (`yyyy`, `MM`) and creates `LeetCode/<yyyy>/<MM>` if missing.
2. Copies `LeetCode/LeetCodeTemplate.cs` to `LeetCode/<yyyy>/<MM>/LeetCode<number>.cs`.
3. Replaces `namespace LeetCodeTemplate;` with `namespace LeetCode<number>;` in the new file.
4. Replaces the `using LeetCode...;` line in `LeetCode/Program.cs` with `using LeetCode<number>;`.

It fails if `LeetCode<number>.cs` already exists anywhere in the project.

The script also fetches problem data from the public LeetCode GraphQL API (`https://leetcode.com/graphql`):
5. Inserts the C# method signature (from the `csharp` code snippet) into the `Solution` class, after `Run()`, with body `throw new NotImplementedException();`.
6. Prints a `=== PROBLEM DATA ===` JSON block with `csharpSnippet`, `metaData` (method name, param names/types, return type), `exampleTestcases` (raw inputs, one parameter per line, grouped by params count) and `expectedOutputs`.

If fetching fails, the script only warns; the file is still created from the template.

## Fill the Run method

Using the printed PROBLEM DATA, replace the placeholder comments in `Run()` of the created file with one call per example, for example:

```csharp
using LeetCode.Common;

namespace LeetCode1;

public class Solution
{
    public void Run()
    {
        TestCase.Check(TwoSum([2, 7, 11, 15], 9), new[] { 0, 1 });
        TestCase.Check(TwoSum([3, 2, 4], 6), new[] { 1, 2 });
    }
```

Rules:
- Split `exampleTestcases` lines into groups of `metaData.params.Length`; the N-th group matches `expectedOutputs[N]`.
- Convert LeetCode literals to C#: `[1,2]` -> collection expression `[1, 2]`, `[[1,2],[3]]` -> `[[1, 2], [3]]` for `int[][]`, `"abc"` stays a string, `'a'` stays a char, `true/false` unchanged.
- Every example must be checked via `TestCase.Check(actual, expected)` from `LeetCode/Common/TestCase.cs` (add `using LeetCode.Common;` at the top of the file). It prints `Case N: <result> -> PASS|FAIL` (plus expected value on FAIL); values longer than 20 characters are truncated with `...`. Do not print results with `Console.WriteLine` directly.
- Pass the expected value as a C# value of the same shape as the result (arrays as `new[] { ... }` / `new[] { new[] { ... } }`, since collection expressions need a target type). Comparison is done by formatted value, so element types/collection types may differ.
- For `ListNode`/`TreeNode` params use helpers from `LeetCode/Common` if available; `ListNode` results are formatted as arrays, so pass the expected value as an array.
- If the problem accepts any order of answer, sort the result before passing it to `Check` and mention it in a comment.
- If the class uses a design-style problem (`metaData` has `classname`/`methods`), write the sequence of calls manually based on the examples.

After running, build the `LeetCode` project to verify, and report the created file path to the user.
