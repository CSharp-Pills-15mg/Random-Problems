// C# Pills 15mg
// Copyright (C) 2021 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DustInTheWind.RandomProblems.MultiThreadingProblem.Business.RandomNumbers
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        private static readonly Random SeedRandom = new();

        /// <summary>
        /// This is a more recent solution. <see cref="ThreadLocal{T}"/> is available from .NET Framework 4.0.
        /// Behind the scene it is automatically creating a single instance of the `Random` class for each thread that requests it.
        /// </summary>
        private static readonly ThreadLocal<Random> ThreadLocalRandom = new(CreateNewRandom);

        private readonly List<int> numbers = new();

        public int this[int index] => numbers[index];

        public int GenerateNext()
        {
            // The `Value` property returns the `Random` instance specific to the current thread.
            int number = ThreadLocalRandom.Value.Next();
            numbers.Add(number);
            return number;
        }

        private static Random CreateNewRandom()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Creating Random instance for thread {threadId}");

            int seed = GenerateNewSeed();
            return new Random(seed);
        }

        private static int GenerateNewSeed()
        {
            lock (SeedRandom)
            {
                // We use a separate `Random` instance (called here `SeedRandom`) only for generating the seed
                // for the real `Random` instance.
                return SeedRandom.Next();
            }
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