using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DustInTheWind.Step04_ThreadLocalSolution
{
    internal class Solution
    {
        private static readonly Random SeedRandom = new Random();

        /// <summary>
        /// This is a more recent solution. <see cref="ThreadLocal{T}"/> is available from .NET Framework 4.0.
        /// </summary>
        private static readonly ThreadLocal<Random> Random = new ThreadLocal<Random>(CreateNewRandom);

        public async Task Execute()
        {
            const int count = 10_000_000;

            Console.WriteLine($"Generating {count:N0} random numbers...");
            List<int> numbers = await GenerateValues(count);

            Console.WriteLine("All numbers are generated. Creating the string of numbers...");
            string numbersAsString = string.Join(", ", numbers);
            Console.WriteLine(numbersAsString);
        }

        private static async Task<List<int>> GenerateValues(int count)
        {
            List<int> numbers = new List<int>();

            List<Task> tasks = Enumerable.Range(0, count)
                .Select(x => Task.Run(() =>
                {
                    int number = Random.Value.Next();

                    lock (numbers)
                        numbers.Add(number);
                }))
                .ToList();

            await Task.WhenAll(tasks);

            return numbers;
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

    }
}