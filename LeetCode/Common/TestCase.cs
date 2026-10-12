using System.Collections;
using System.Globalization;
using System.Text;

namespace LeetCode.Common
{
    public static class TestCase
    {
        private const int MaxLength = 20;
        private static int _counter;

        public static void Check(object? actual, object? expected)
        {
            _counter++;
            var actualText = Format(actual);
            var expectedText = Format(expected);
            var passed = actualText == expectedText;

            var line = $"Case {_counter}: {Truncate(actualText)} -> {(passed ? "PASS" : "FAIL")}";
            if (!passed) line += $" (expected: {Truncate(expectedText)})";

            var color = Console.ForegroundColor;
            Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(line);
            Console.ForegroundColor = color;
        }

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

        private static string Truncate(string text) =>
            text.Length <= MaxLength ? text : text[..MaxLength] + "...";
    }
}
