using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DustInTheWind.Step04_ThreadLocalSolution
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        private static readonly Random SeedRandom = new Random();

        /// <summary>
        /// This is a more recent solution. <see cref="ThreadLocal{T}"/> is available from .NET Framework 4.0.
        /// </summary>
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(CreateNewRandom);

        private readonly List<int> numbers = new List<int>();

        public int Count => numbers.Count;

        public int this[int index] => numbers[index];

        public int GenerateNext()
        {
            int number = Random.Value.Next();
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