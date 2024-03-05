using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode948
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(BagOfTokensScore(new int[] { 200, 100}, 150));
        }
        public int BagOfTokensScore(int[] tokens, int power)
        {
            Array.Sort(tokens);

            var left = 0;
            var right = tokens.Length - 1;
            var score = 0;
            var MaxScore = 0;

            while (right >= left)
            {
                if (power >= tokens[left])
                {
                    power -= tokens[left];
                    score++;
                    left++;
                } 
                else if (score >= 1)
                {
                    power += tokens[right];
                    score--;
                    right--;
                } 
                else
                {
                    break;
                }      

                MaxScore = score > MaxScore ? score : MaxScore;
            }

            return MaxScore;
        }
    }
}
