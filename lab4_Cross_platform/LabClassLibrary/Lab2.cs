using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LabClassLibrary
{
    public static class Lab2
    {
        static bool IsValid(string s)
        {
            if (s.Length==0)
            {
                Console.WriteLine("Error! The set is empty.");
                return false;
            }

            if (s.Split(' ').Length > 1)
            {
                Console.WriteLine("Error! Space(s) found in set."); 
                return false;
            }

            bool containsCapitalLetters = Regex.IsMatch(s, "[A-Z]");
            if (containsCapitalLetters)
            {
                Console.WriteLine("Error! The set contains capital letter(s).");
                return false;
            }

            if (s.Length>70)
            {
                Console.WriteLine("Error! The number of characters in the set is greater than 70.");
                return false;
            }
            return true;
        }

        static bool IsPalindrome(string s, int start, int len)
        {
            int end = start + len - 1;
            while (start < end)
            {
                if (s[start] != s[end]) return false;
                start++;
                end--;
            }
            return true;
        }

        static (int, List<string>) FindPalindromes(string line)
        {
            int n = line.Length;
            int[] dp = new int[n + 1];
            int[] partition = new int[n + 1];
            dp[0] = 0;

            for (int i = 1; i <= n; i++)
            {
                dp[i] = int.MaxValue;
                for (int j = 0; j < i; j++)
                {
                    if (IsPalindrome(line, j, i - j) && dp[j] + 1 < dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        partition[i] = j;
                    }
                }
            }

            List<string> palindromes = new List<string>();
            int index = n;
            while (index > 0)
            {
                palindromes.Add(line.Substring(partition[index], index - partition[index]));
                index = partition[index];
            }

            palindromes.Reverse();

            return (dp[n], palindromes);
        }

        public static void ExecuteLab2(string inputFilePath, string outputFilePath)
        {
            try
            {
                string line = File.ReadAllText(inputFilePath).Trim();
                if (IsValid(line) == false)
                    return;
                (int k, List<string> palindromes) = FindPalindromes(line);
                File.WriteAllText(outputFilePath, $"{k}\n{string.Join("\n", palindromes)}");
            } 
            catch (Exception) 
            {
                Console.WriteLine("The file was not found");
            }
        }
    }
}