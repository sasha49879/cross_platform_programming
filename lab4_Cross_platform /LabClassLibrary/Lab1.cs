using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

namespace LabClassLibrary
{
    public static class Lab1
    {
        private static long _maxUtility = 0;
        private static int _minCount = int.MaxValue;
        private static List<int> _selectedItems = new List<int>();
        private static int _itemCount, _maxWeight;
        private static int[] _weights, _utilities;

        private static void Search(int index, int currentWeight, long currentUtility, int currentCount, List<int> currentItems)
        {
            if (currentWeight > _maxWeight) return;
            if (currentUtility > _maxUtility || (currentUtility == _maxUtility && currentCount < _minCount))
            {
                _maxUtility = currentUtility;
                _minCount = currentCount;
                _selectedItems = new List<int>(currentItems);
            }

            if (index == _itemCount) return;

            currentItems.Add(index + 1);
            Search(index + 1, currentWeight + _weights[index], currentUtility + _utilities[index],
                currentCount + 1,
                currentItems);

            currentItems.RemoveAt(currentItems.Count - 1);
            Search(index + 1, currentWeight, currentUtility, currentCount, currentItems);
        }


        public static void ExecuteLab1(string inputFilePath, string outputFilePath)
        {
            string[] lines = File.ReadAllLines(inputFilePath);
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

            Search(0, 0, 0, 0, new List<int>());

            File.WriteAllText(outputFilePath, $"{_minCount} {_maxUtility}\n{string.Join(" ", _selectedItems)}");

        }
    }

}