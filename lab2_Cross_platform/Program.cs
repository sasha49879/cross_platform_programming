/*
 * Кросплатформне програмування
 * Лабораторна робота 2
 * ІПЗ-41
 * Швиданенко Олександр Олексійович
 * 72 варіант
 *
 * Завдання:
 * Заклинання Ауерса є набором звуків, записуваних малими літерами англійського алфавіту без пробілів.
 * Сам по собі цей набір не має ні секретності, ні магічної сили і наведено у всіх підручниках з білої магії у відповідному розділі.
 * Там же вказано спосіб його застосування:
 * а) розбити набір букв на паліндроми, тобто непусті слова, які читаються однаково як праворуч, так і зліва направо;
 * б) вимовляти їх у порядку, після кожного, змахуючи умклайдетом (у разі, клацаючи пальцями);
 * в) магічна сила заклинання обернено пропорційна числу паліндромів, на які розбитий набір букв.
 * ...
 * Вхідні дані
 * У єдиному рядку файлу INPUT.TXT міститься непустий вихідний набір, що складається з маленьких букв англійського алфавіту без пробілів
 * (кількість символів у наборі не більше 70).
 * Вихідні дані
 * У першому рядку вихідного файлу OUTPUT.TXT необхідно вказати найменше число k паліндромів, на які розбивається набір.
 * Ці k паліндромів потрібно навести в наступних k рядках у порядку, в якому вони входять у вихідний набір і повинні вимовлятися
 * при використанні заклинання. Якщо є кілька мінімальних розбиття, достатньо вивести будь-яке з них.
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace lab2_Cross_platform
{
    class Program
    {
        // перевіка, чи задовольняє вхідний рядок заданим умовам.
        static bool IsValid(string s)
        {
            // якщо вхідний рядок пустий
            if (s.Length==0)
            {
                Console.WriteLine("Error! The set is empty.");
                return false;
            }

            // якщо вхідний рядок містить пропуски
            if (s.Split(' ').Length > 1)
            {
                Console.WriteLine("Error! Space(s) found in set."); 
                return false;
            }

            // якщо вхідний рядок містить великі літери
            bool containsCapitalLetters = Regex.IsMatch(s, "[A-Z]");
            if (containsCapitalLetters)
            {
                Console.WriteLine("Error! The set contains capital letter(s).");
                return false;
            }

            // якщо довжина вхідного рядка перевищує 70
            if (s.Length>70)
            {
                Console.WriteLine("Error! The number of characters in the set is greater than 70.");
                return false;
            }
            return true;
        }

        // перевірка на паліндром
        static bool IsPalindrome(string s, int start, int len)
        {
            int end = start + len - 1;
            while (start < end)
            {
                // повертаємо false, якщо символи в симетричних позиціях відрізняються
                if (s[start] != s[end]) return false;
                start++;
                end--;
            }
            return true;
        }

        // знаходження найменшої кількісті паліндромів
        static (int, List<string>) FindPalindromes(string line)
        {
            int n = line.Length;
            // масив `dp` зберігає мінімально необхідну кількість паліндромів для побудови підрядка `line[0...i]`
            int[] dp = new int[n + 1];
            // Масив `partition` зберігає позицію, з якої починається останній паліндром в оптимальному розбитті підрядка `line[0...i]`
            int[] partition = new int[n + 1];
            dp[0] = 0;

            // динамічний цикл програмування для заповнення масивів `dp` та `partition`
            for (int i = 1; i <= n; i++)
            {
                dp[i] = int.MaxValue;
                for (int j = 0; j < i; j++)
                {
                    // якщо `line[j...i-1]` є паліндромом, то оновлюється `dp[i]` та `partition[i]`
                    if (IsPalindrome(line, j, i - j) && dp[j] + 1 < dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        partition[i] = j;
                    }
                }
            }

            //побудова паліндрому, використовуючи інформацію з `partition`
            List<string> palindromes = new List<string>();
            int index = n;
            while (index > 0)
            {
                palindromes.Add(line.Substring(partition[index], index - partition[index]));
                index = partition[index];
            }

            // реверс списоку паліндромів, оскільки ми побудували його задом наперед
            palindromes.Reverse();

            // повертаємо мінімальну кількість паліндромів та самі паліндроми
            return (dp[n], palindromes);
        }

        static void Main()
        {
            try
            {
                // зчитування вхідних даних з файлу
                string line = File.ReadAllText("INPUT.TXT").Trim();

                // перевірка вхідних даних
                if (IsValid(line) == false)
                    return;

                // знаходження мінімальної кількісті паліндромів та самі паліндроми
                (int k, List<string> palindromes) = FindPalindromes(line);

                // запис вихідних даних в файл
                File.WriteAllText("OUTPUT.TXT", $"{k}\n{string.Join("\n", palindromes)}");
            } 
            catch (Exception) 
            {
                // помилка про зчитування файлу
                Console.WriteLine("The file was not found");
            }
        }
    }
}
