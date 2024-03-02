using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode150
{
    public class Solution
    {

        private (int, int) Calc(string[] tokens, int index)
        {
            int value;
            if (int.TryParse(tokens[index], out value))
            {
                return (value, 1);
            }

            var (token2, size2) = Calc(tokens, index - 1);
            var (token1, size1) = Calc(tokens, index - 1 - size2);

            switch (tokens[index])
            {
                case "+": value = token1 + token2; break;
                case "-": value = token1 - token2; break;
                case "*": value = token1 * token2; break;
                case "/": value = token1 / token2; break;
            }

            return (value, size1 + size2 + 1);
        }

        public int EvalRPN(string[] tokens)
        {
            return Calc(tokens, tokens.Length - 1).Item1;
        }
    }
}
