using System;
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.TheCriticalResource
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        private static readonly Random random = new Random();
        private readonly List<int> numbers = new List<int>();

        public int Count => numbers.Count;

        public int this[int index] => numbers[index];

        public int GenerateNext()
        {
            int number = random.Next();
            numbers.Add(number);
            return number;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return numbers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}