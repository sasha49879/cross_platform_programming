/*
 * Кросплатформне програмування
 * Лабораторна робота 1
 * ІПЗ-41
 * Швиданенко Олександр Олексійович
 * 72 варіант
 *
 * Завдання:
 * Одним із класичних NP-повних завдань є так зване «Завдання про рюкзак». Формулюється вона в такий спосіб.
 * Дано n предметів, кожен з яких характеризується вагою wi та корисністю pi. Необхідно вибрати деякий набір цих предметів так,
 * щоб сумарна вага цього набору не перевищувала W, а сумарна корисність була максимальною.
 * Ваше завдання полягає в тому, щоб написати програму, яка вирішує завдання про рюкзак.
 * Вхідні дані
 * Перший рядок вхідного файлу INPUT.TXT містить натуральні числа n (1 ≤ n ≤ 20) та W (1 ≤ W ≤ 109).
 * Кожен із наступних n рядків містить опис одного предмета. Кожен опис складається з двох чисел: wi – ваги предмета та pi – його корисності (1 ≤ wi, pi ≤ 109).
 * Вихідні дані
 * У першому рядку вихідного файлу OUTPUT.TXT виведіть кількість вибраних предметів та їхню сумарну корисність.
 * У другому рядку виведіть через пробіл їх номери у порядку, що зростає (предмети нумеруються з одиниці в порядку,
 * в якому вони перераховані у вхідному файлі). Якщо кілька наборів, виберіть той, у якому найменша кількість предметів.
 * Якщо ж після цього відповідь, як і раніше, неоднозначна, виберіть той набір, у якому перший предмет має найменший можливий номер,
 * з усіх таких виберіть той, у якому другий предмет має найменший можливий номер, і т.д.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace lab1_Cross_platform
{
    class Program
    { 
        private static long _maxUtility = 0;
        private static int _minCount = int.MaxValue;
        private static List<int> _selectedItems = new List<int>();
        private static int _itemCount, _maxWeight;
        private static int[] _weights, _utilities;

        // метод пошуку комбінації предметів, яка максимізує корисність, не перевищуючи при цьому вагових обмежень
        private static void Search(int index, int currentWeight, long currentUtility, int currentCount, List<int> currentItems)
        {
            // якщо поточна вага перевищує максимально допустиму, завершити пошук
            if (currentWeight > _maxWeight) return;

            // якщо поточна корисність є найкращою на даний момент, або є такою ж, але з меншою кількістю елементів, оновити
            if (currentUtility > _maxUtility || (currentUtility == _maxUtility && currentCount < _minCount))
            {
                _maxUtility = currentUtility;
                _minCount = currentCount;
                _selectedItems = new List<int>(currentItems);
            }

            // якщо не залишилося жодного елемента, завершити
            if (index == _itemCount) return;

            // включити поточний елемент і рекурсивно шукати далі
            currentItems.Add(index + 1);
            Search(index + 1, currentWeight + _weights[index], currentUtility + _utilities[index],
                currentCount + 1,
                currentItems);

            // видалити поточний елемент і рекурсивно шукати далі
            currentItems.RemoveAt(currentItems.Count - 1);
            Search(index + 1, currentWeight, currentUtility, currentCount, currentItems);
        }

        static void Main()
        {

            // читання вхідних даних з файлу 'INPUT.TXT'
            string[] lines = File.ReadAllLines("INPUT.TXT");
            var input = lines[0].Split(' ');
            BigInteger upperLimit = new BigInteger(Math.Pow(10,9));
            _itemCount = int.Parse(input[0]);
            if (_itemCount.ToString() != input[0] || _itemCount < 1 || _itemCount > 21)
            {
                Console.WriteLine("Error! The number of items must be an integer in the range from 1 to 20!"); 
                return;
            }
            
            _maxWeight = int.Parse(input[1]);
            if (_maxWeight.ToString() != input[1] || _maxWeight < 1 || _maxWeight > upperLimit)
            {
                Console.WriteLine("Error! The total weight of the items must be an integer in the range from 1 to 10^9!"); 
                return;
            }
            _weights = new int[_itemCount];
            _utilities = new int[_itemCount];

            // розбір ваги та корисності елементів з вхідних даних
            for (int i = 0; i < _itemCount; i++)
            {
                var item = lines[i + 1].Split(' ');
                _weights[i] = int.Parse(item[0]);
                _utilities[i] = int.Parse(item[1]);
                if (_weights[i].ToString() != item[0] || _weights[i] < 1 || _weights[i] > upperLimit)
                {
                    Console.WriteLine("Error! The weight of the item must be an integer in the range from 1 to 10^9!"); 
                    return;
                }
                if (_utilities[i].ToString() != item[1] || _utilities[i] < 1 || _utilities[i] > upperLimit)
                {
                    Console.WriteLine("Error! The utilities of the item must be an integer in the range from 1 to 10^9!"); 
                    return;
                }
            }

            // початок рекурсивного пошуку
            Search(0, 0, 0, 0, new List<int>());

            // запис результату у файл 'OUTPUT.TXT'
            File.WriteAllText("OUTPUT.TXT", $"{_minCount} {_maxUtility}\n{string.Join(" ", _selectedItems)}");
        }
    }
}
