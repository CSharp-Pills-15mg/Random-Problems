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

namespace DustInTheWind.RandomProblems.Business.RandomNumbers
{
    internal class RandomNumbersList : IEnumerable<int>
    {
        private static readonly Random SeedRandom = new();

        /// <summary>
        /// This is the oldest solution. <see cref="ThreadStaticAttribute"/> is available from .NET Framework 1.1.
        /// A single `Random` instance exists for each thread.
        /// </summary>
        [ThreadStatic]
        private static Random random;

        private readonly List<int> numbers = new();

        public int this[int index] => numbers[index];

        public int GenerateNext()
        {
            // If, on the current thread, the thread-static instance was not created yet, we create a new one.
            random ??= CreateNewRandom();

            int number = random.Next();
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