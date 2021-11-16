using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DustInTheWind.Step03_ThreadStaticSolution
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        private static readonly Random SeedRandom = new Random();

        /// <summary>
        /// This is the oldest solution. <see cref="ThreadStaticAttribute"/> is available from .NET Framework 1.1.
        /// </summary>
        [ThreadStatic]
        private static Random random;

        private readonly List<int> numbers = new List<int>();

        public int Count => numbers.Count;

        public int this[int index] => numbers[index];

        public int GenerateNext()
        {
            if (random == null)
                random = CreateNewRandom();

            int number = random.Next();
            numbers.Add(number);
            return number;
        }

        private static Random CreateNewRandom()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Creating Random instance for thread {threadId}");

            int seed;

            lock (SeedRandom)
                seed = SeedRandom.Next();

            return new Random(seed);
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