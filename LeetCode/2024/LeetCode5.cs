using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode5
{
    public class Solution
    {
        public void Run()
        {
            Console.WriteLine(LongestPalindrome("babad"));
        }

        public string LongestPalindrome(string s)
        {
            var maxLength = 0;
            var pal = "";
            var n = s.Length;
            for (int i = 0, j = 0; i < n; i++)
            {
                for (j = 1; i - j >= 0 && i + j < n; j++)
                {
                    if (s[i - j] != s[i + j])
                    {
                        break;
                    }
                }

                j--;

                if (j >= 0 && j * 2 + 1 > maxLength)
                {
                    maxLength = j * 2 + 1;
                    pal = s.Substring(i - j, maxLength);
                }

                if (i + 1 < n && s[i + 1] == s[i])
                {
                    for (j = 0; i - j >= 0 && i + j + 1 < n; j++)
                    {
                        if (s[i - j] != s[i + j + 1])
                        {
                            break;
                        }
                    }
                    
                    j--;

                    if (j >= 0 && j * 2 + 2 > maxLength)
                    {
                        maxLength = j * 2 + 2;
                        pal = s.Substring(i - j, maxLength);
                    }
                }
            }

            return pal;
        }
    }
}
