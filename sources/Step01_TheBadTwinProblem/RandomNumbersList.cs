using System;
using System.Collections;
using System.Collections.Generic;

namespace DustInTheWind.TheBadTwin
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        private readonly Random random = new Random();
        private readonly List<int> numbers = new List<int>();

        public int Count => numbers.Count;

        public int LastNumber
        {
            get
            {
                if (numbers.Count == 0)
                    throw new Exception("No number was generated yet.");

                return numbers[^1];
            }
        }

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