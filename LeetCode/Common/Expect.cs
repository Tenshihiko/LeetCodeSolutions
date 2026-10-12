using System.Collections;
using System.Globalization;
using System.Text;
using Xunit;

namespace LeetCode.Common
{
    public static class Expect
    {
        /// <summary>
        /// Pass as expected value when the correct answer is unknown: any result is accepted.
        /// </summary>
        public const string Any = "<any>";

        /// <summary>
        /// Compares values by their LeetCode-like text representation, so collection types
        /// (int[] vs IList&lt;int&gt;, jagged arrays, ListNode vs array) may differ.
        /// </summary>
        public static void Equal(object? expected, object? actual)
        {
            if (expected is string s && s == Any) return;
            Assert.Equal(Format(expected), Format(actual));
        }

        /// <summary>
        /// Same as <see cref="Equal"/> with (actual, expected) order; without expected any result is accepted.
        /// </summary>
        public static void Check(object? actual) => Equal(Any, actual);

        public static void Check(object? actual, object? expected) => Equal(expected, actual);

        public static string Format(object? value)
        {
            switch (value)
            {
                case null: return "null";
                case string s: return $"\"{s}\"";
                case char c: return $"\"{c}\"";
                case bool b: return b ? "true" : "false";
                case double d: return d.ToString("0.#####", CultureInfo.InvariantCulture);
                case float f: return f.ToString("0.#####", CultureInfo.InvariantCulture);
                case ListNode node:
                    var items = new List<string>();
                    for (var n = node; n != null; n = n.next) items.Add(n.val.ToString());
                    return $"[{string.Join(",", items)}]";
                case IEnumerable enumerable:
                    var sb = new StringBuilder("[");
                    var first = true;
                    foreach (var item in enumerable)
                    {
                        if (!first) sb.Append(',');
                        sb.Append(Format(item));
                        first = false;
                    }
                    return sb.Append(']').ToString();
                default: return Convert.ToString(value, CultureInfo.InvariantCulture) ?? "null";
            }
        }
    }
}
